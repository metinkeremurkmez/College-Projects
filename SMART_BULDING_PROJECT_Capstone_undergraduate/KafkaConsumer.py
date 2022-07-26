import findspark

findspark.init("/opt/manual/spark")

from pyspark.ml.pipeline import PipelineModel
from pyspark.sql import SparkSession, functions as F

spark = (SparkSession.builder
         .appName("Read From Kafka")
         .config("spark.jars.packages", "org.apache.spark:spark-sql-kafka-0-10_2.12:3.1.1,org.elasticsearch:elasticsearch-spark-30_2.12:7.12.1")
         .config("spark.serializer", "org.apache.spark.serializer.KryoSerializer")
         .config("es.index.auto.create", "true")
         .getOrCreate()
         )

spark.sparkContext.setLogLevel('ERROR')

lines = (spark.readStream
         .format("kafka")
         .option("kafka.bootstrap.servers", "localhost:9092")
         .option("subscribe", "office-input")
         .load())

lines2 = lines.selectExpr("CAST(key AS STRING)", "CAST(value AS STRING)")

lines3 = lines2.withColumn("Value_co2", F.split(F.col("value"), ",")[2]) \
    .withColumn("Value_humidity", F.split(F.col("value"), ",")[3]) \
    .withColumn("Value_temperature", F.split(F.col("value"), ",")[4]) \
    .withColumn("Value_light", F.split(F.col("value"), ",")[5]) \
    .withColumn("Room", F.split(F.col("value"), ",")[0]) \
    .withColumn("Time", F.split(F.col("value"), ",")[1]) \
    .selectExpr("CAST(Value_co2 AS FLOAT)", "CAST(Value_humidity AS FLOAT)"
                                     , "CAST(Value_temperature AS FLOAT)"
                                     , "CAST(Value_light AS FLOAT)", "value", "Room", "Time")

model = PipelineModel.load('~/saved_models/Clf_Tuned_Decision_Tree')

transformed_frame = model.transform(lines3) \
     .selectExpr('Value_co2', 'Value_humidity', 'Value_temperature', 'Value_light', "Room", "Time", "prediction")

checkpoint_dir = "file:///home/train/project_fin/streaming_logs"
"""
streamingQuery = (transformed_frame
                  .selectExpr("to_json(struct(*)) AS value")
                  .writeStream
                  .format("kafka")
                  .option("kafka.bootstrap.servers", "localhost:9092")
                  .option("topic", "office-activity")
                  .outputMode("append")
                  .option("checkpointLocation", checkpoint_dir)
                  .start())
"""
streamingQuery = (transformed_frame
                  #.selectExpr("to_json(struct(*)) AS value") ,, bunu calistiricaksan transformed_frame üzerinde value da selectexpr at
                  .writeStream
                  .format("org.elasticsearch.spark.sql")
                  .outputMode("append")
                  .option("es.nodes", "localhost")
                  .option("es.port", "9200")
                  .option("checkpointLocation", checkpoint_dir)
                  .start("smart-building-sensor"))

streamingQuery.awaitTermination()


#python dataframe_to_kafka.py --input ~/tez/test.csv/part-00000-0ebb56bc-34bf-4ed0-995e-5f78050631cc-c000.csv --topic office-input


