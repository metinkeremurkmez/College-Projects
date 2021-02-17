use airline;
ALTER TABLE COMPANY
ADD CONSTRAINT CHK_both_zero
CHECK(not(Airplane_company = 0 and Airline_company = 0)),
ADD CONSTRAINT CHK_airplane_company
CHECK ((Airplane_company = 1 and Num_of_produced_airplane is not null)
or (Airplane_company = 0 and Num_of_produced_airplane is null) ),
ADD CONSTRAINT CHK_airline_company
CHECK ((Airline_company = 1 and Num_of_used_airplane is not null)
or (Airline_company = 0 and Num_of_used_airplane is null) );


use airline;
ALTER TABLE FLIGHT
ADD CHECK
(Weekdays IN ("MON","TUE","WED","THU","FRI","SAT","SUN"));


use airline;
ALTER TABLE FLIGHT_LEG
ADD CONSTRAINT CHK_Leg CHECK ( (Leg_number>0 AND Leg_number<=5) AND (Mileage>=200 AND Mileage<=2200));


use airline;
ALTER TABLE FARE
ADD CHECK (Amount>=0);


use airline;
ALTER TABLE CHECK_IN
ADD CHECK (Check_in_time <= CHECK_IN.Date);


