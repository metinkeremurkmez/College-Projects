INSERT INTO airline.CUSTOMER (Passport_no, Cust_name, Phone, Email, Address, Country)
VALUES ( 432100000, 'Sena Yıldız', 55576664466, 'sena@gmail.com', 'Mugla', 'Turkey'),
(321000000, 'Fatma Dogan', 5446725533, 'fatma@gmail.com', 'Istanbul', 'Turkey');
DELETE FROM airline.CUSTOMER
Where phone = 5446725533 ;
UPDATE airline.CUSTOMER
SET
email = 'berk.dogan@gmail.com'
WHERE
passport_no = 654321000;


INSERT INTO airline.FFP(Ffp_id, Ffp_name, Ffp_Status, Company_id)
VALUES(4438, 'Aadvantage', 'B', 5678);
DELETE FROM airline.FFP
Where ffp_name = 'Mileageplus' ;
UPDATE airline.FFP
SET
ffp_name = 'Sky-pass'
WHERE
ffp_name = 'Skypass';


INSERT INTO airline.AIRPLANE_TYPE (Airplane_type_name, Max_seats, Company_id)
VALUES ('Airbus 11', 140, 4567 );
DELETE FROM airline.airplane_type
Where max_seats = 140 ;
UPDATE airline.airplane_type
SET
Airplane_type_name = 'Toupolev 89'
WHERE
Airplane_type_name = 'Tupolev';


INSERT INTO airline.SEAT_RESERVATION (Flight_number, Leg_number, Leg_Date, Seat_number, Passport_no)
VALUES
(111, 2, '2021-04-10 14:00:00', 7, 543210000);
DELETE FROM airline.seat_reservation
WHERE passport_no = 987654321 and leg_date = '2021-10-10 08:00:00';
UPDATE airline.seat_reservation
SET
seat_number = 20
WHERE
Leg_date = '2021-03-10 15:00:00' and passport_no= 876543210;


INSERT INTO airline.FARE(Flight_number, Fare_code, Restrictions, Amount)
VALUES (117, 'CFP4TA', 'Non-refundable', 1700),
(118, 'DYT9DF', 'Refundable', 950);
DELETE FROM airline.fare
WHERE Flight_number = 117;
UPDATE airline.fare
SET
amount = 430,
restrictions= 'Refundable'
WHERE
Fare_code = 'C53200';


use airline;
SELECT COUNT(*)
FROM Airport , Can_land
WHERE Airport.Airport_name='Dothan Airport' and
Airport.Airport_code = Can_land.Airport_code;


use airline;
SELECT Company.Company_name,COUNT(*)
FROM Company,flight
WHERE Company.Company_id = Flight.Company_id
GROUP BY(Company.Company_name);


use airline;
SELECT Flight.Weekdays
FROM Flight,Fare
WHERE Flight.Flight_number = Fare.Flight_number and
Fare.Amount = (SELECT MAX(Amount) FROM Fare);


use airline;
SELECT Company.Company_name, COUNT(*)
FROM Company,FFP,Member_of
WHERE Company.Company_id = FFP.Company_id and
FFP.FFP_id = Member_of.FFP_id
GROUP BY (Company.Company_name);


use airline;
SELECT fare.Amount
FROM flight_leg,flight,fare
WHERE flight_leg.Flight_number = flight.Flight_number and
flight.Flight_number = fare.Flight_number and
flight_leg.Mileage = (SELECT MAX(Mileage) FROM flight_leg);


use airline;
SELECT DISTINCT(airport.Airport_name)
FROM airport,can_land,airplane
WHERE airport.airport_code = can_land.airport_code and
can_land.Airplane_type_name = airplane.Airplane_type and
airplane.Total_number_of_seats = (SELECT MAX(Total_number_of_seats) FROM airplane);


use airline;
SELECT Company.Company_name,COUNT(airplane.Airplane_id)
FROM Company,airplane_type,airplane
WHERE Company.Company_id = airplane_type.Company_id and
airplane_type.Airplane_type_name = airplane.Airplane_type
GROUP BY Company.Company_name;


use airline;
SELECT company.Company_name, COUNT(*)
FROM company, flight,flight_leg,leg_instance
WHERE company.Company_id = flight.Company_id and
flight.Flight_number = flight_leg.Flight_number and
flight_leg.Flight_number = leg_instance.Flight_number and
flight_leg.Leg_number = leg_instance.Leg_number
GROUP BY (Company.Company_name);


use airline;
SELECT Company.Company_name,COUNT(DISTINCT( airplane.Airplane_type))
FROM Company,flight,leg_instance,airplane
WHERE Company.Company_id = flight.Company_id and
flight.Flight_number = leg_instance.Flight_number and
leg_instance.Airplane_id = airplane.Airplane_id
GROUP BY Company.Company_name;


use airline;
SELECT Customer.Passport_no,Customer.Cust_name,SUM(fare.Amount)
FROM customer, flight,fare,seat_reservation
WHERE seat_reservation.Flight_number = flight.Flight_number AND
flight.Flight_number = fare.Flight_number and
seat_reservation.Passport_no = customer.Passport_no
GROUP BY(seat_reservation.Passport_no) ORDER BY SUM(fare.Amount) DESC;


use airline;
SELECT Company_name
FROM Airplane_type,Company
WHERE Airplane_type.Company_id = Company.Company_id AND
Airplane_type.Max_seats = (SELECT MAX(Airplane_type.Max_seats)
FROM Airplane_type
);


use airline;
SELECT L.Flight_number,L.Leg_number,L.Leg_date
FROM leg_instance AS L
WHERE L.Arrival_airport_code not in
(SELECT DISTINCT (can_land.Airport_code)
FROM airplane,can_land
WHERE L.Airplane_id = airplane.Airplane_id and
Can_land.Airplane_type_name = airplane.Airplane_type );


SELECT Cust_name, Email
FROM airline.customer
WHERE EXISTS (SELECT Passport_no FROM Check_in WHERE
customer.passport_no = check_in.Passport_no);


SELECT A.Airplane_type_name
FROM airplane_type AS A
WHERE EXISTS(SELECT * FROM can_land AS C
WHERE A.Airplane_type_name = C.Airplane_type_name);


use airline;
SELECT Cust_name
FROM customer AS C
WHERE EXISTS (SELECT * FROM seat_reservation AS S
WHERE S.Passport_no = C.Passport_no AND
NOT EXISTS(SELECT * FROM check_in AS CI
WHERE CI.Passport_no = C.Passport_no AND
S.Flight_number = CI.Flight_number AND
S.Leg_number = CI.Leg_number AND
S.Leg_date = CI.Leg_date));


SELECT Passport_no, Mileage
FROM airline.check_in
WHERE NOT EXISTS (
SELECT passport_no
FROM member_of
WHERE check_in.passport_no = member_of.Passport_no);


use airline;
SELECT A.Airport_name
FROM airport AS A
WHERE NOT EXISTS(SELECT * FROM can_land AS C
WHERE A.Airport_code = C.Airport_code);


SELECT Cust_name
FROM customer
WHERE NOT EXISTS(SELECT * FROM ffc
WHERE customer.Passport_no = ffc.Passport_no);


SELECT fare.fare_code, flight.weekdays
FROM airline.FLIGHT
LEFT JOIN fare ON flight.flight_number = fare.flight_number;


SELECT *
FROM airline.customer
RIGHT JOIN seat_reservation ON customer.passport_no = seat_reservation.passport_no
ORDER BY seat_reservation.leg_date;






