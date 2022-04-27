INSERT INTO Employee (ssn, name, salary, union_mem_no, phone_no, street, zip, state, city)
VALUES (140552179, 'Arie', 316563.60, 4580878253, 9164589619, '8184 Gravel Circle', 18637, 'Illinois', 'Sunnyvale');
INSERT INTO Employee (ssn, name, salary, union_mem_no, phone_no, street, zip, state, city)
VALUES (964317946, 'Hermila', 666429.86, 1955618776, 1247611142, '2416 Ansdell Lane', 34227, 'Oregon', 'Norman');
INSERT INTO Employee (ssn, name, salary, union_mem_no, phone_no, street, zip, state, city)
VALUES (464200354, 'Hanna', 802996.64, 9098193842, 0672071593, '0441 Corporation Road', 00395, 'New Hampshire', 'Phoenix');
INSERT INTO Employee (ssn, name, salary, union_mem_no, phone_no, street, zip, state, city)
VALUES (476763313, 'Hassie', 945884.78, 1153457879, 9144550837, '0059 Bee Road', 41844, 'Minnesota', 'Oceanside');
INSERT INTO Employee (ssn, name, salary, union_mem_no, phone_no, street, zip, state, city)
VALUES (599588874, 'Foster', 859299.07, 4983076317, 9786295633, '5245 Deans Avenue', 30778, 'Indiana', 'Havre de Grace');
INSERT INTO Employee (ssn, name, salary, union_mem_no, phone_no, street, zip, state, city)
VALUES (875676759, 'Deb', 997993.57, 9648466242, 1812082639, '2005 Higginson Lane', 41943, 'Washington', 'Orlando');
INSERT INTO Employee (ssn, name, salary, union_mem_no, phone_no, street, zip, state, city)
VALUES (366031425, 'Kum', 365323.92, 5999923882, 0917225344, '3205 Plover Circle', 41866, 'Oklahoma', 'Norfolk');
INSERT INTO Employee (ssn, name, salary, union_mem_no, phone_no, street, zip, state, city)
VALUES (780178603, 'Ed', 189703.56, 5599745307, 4709623083, '4297 Hurst Street', 52158, 'Montana', 'Henderson');
INSERT INTO Employee (ssn, name, salary, union_mem_no, phone_no, street, zip, state, city)
VALUES (952730700, 'Scarlet', 649492.99, 7155468328, 8693337287, '0424 Clelland Lane', 67596, 'South Carolina', 'New Bedford');
INSERT INTO Employee (ssn, name, salary, union_mem_no, phone_no, street, zip, state, city)
VALUES (283175590, 'Francie', 200458.42, 6345114568, 4095347821, '6212 Westhoughton Lane', 22052, 'Michigan', 'Lorain');
INSERT INTO Test_Info (test_no, name, max_score)
VALUES (0, 'Wings', 10), (1, 'Engine', 20), (2, 'Landing Gear', 15), (3, 'Fuel Tank', 100);
INSERT INTO Model (model_no, capacity, weight)
VALUES (0, 200, 200000), (1, 150, 150000), (2, 300, 300000);
INSERT INTO Airplane (registration_no, model_no)
VALUES (123456789, 0), (3838383838, 0), (8282828282, 0), (1, 0), (53, 1), (2, 1), (3, 1), (8, 2), (52,2), (43, 2);
INSERT INTO Technician (ssn)
VALUES (952730700), (283175590), (780178603), (875676759), (476763313);
INSERT INTO Test (test_no, registration_no, ssn, date, no_hours, score)
VALUES (0, 1, 952730700, '2020-08-20', 7, 5.5), (1, 1, 952730700, '2020-08-21', 7, 7.34), (2, 1, 952730700, '2020-08-22', 4, 12.45), (0, 53, 283175590, '2021-04-05', 3, 8.25), (1, 53, 283175590, '2021-04-06', 5, 15.67), (2, 53, 283175590, '2021-04-07', 6, 12.32), (3, 53, 283175590, '2021-04-08', 5, 89.41);
INSERT INTO Expertise_In (ssn, model_no)
VALUES (952730700, 0), (283175590, 1);
INSERT INTO Traffic_Controller (ssn, most_recent_exam_date)
VALUES (140552179, '2022-02-04'), (964317946, '2020-07-30');

