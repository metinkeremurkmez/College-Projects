CREATE VIEW getreservations AS
SELECT
customer.Cust_name AS 'Customer name',
customer.Phone AS 'Phone',
seat_reservation.Flight_number AS 'Flight number',
seat_reservation.Leg_number AS 'Leg number',
seat_reservation.Leg_Date AS 'Leg date'
FROM
(airline.customer
JOIN seat_reservation ON ((customer.Passport_no = seat_reservation.Passport_no)));


use airline;
CREATE VIEW inilebilecek_airportlar
AS
SELECT can_land.Airplane_type_name,airport.Airport_name
FROM can_land,airport
WHERE can_land.Airport_code = airport.Airport_code;


use airline;
CREATE VIEW upcoming_flights_for_each_company
AS
SELECT company.Company_name,flight.Flight_number
FROM company,flight,leg_instance
WHERE company.Company_id = flight.Company_id and
flight.Flight_number = leg_instance.Flight_number and
leg_instance.Leg_date > current_date();


CREATE VIEW get_ffc_status
AS
SELECT Cust_name,ffp_status,company_name
FROM customer,ffc,member_of,ffp,company
WHERE customer.Passport_no = ffc.Passport_no and
ffc.Passport_no = member_of.Passport_no and
member_of.Ffp_id = ffp.Ffp_id and
ffp.Company_id = company.Company_id


