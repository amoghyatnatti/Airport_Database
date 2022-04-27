CREATE TABLE Employee (
  ssn INT PRIMARY KEY,
  name VARCHAR(30) NOT NULL,
  salary DECIMAL(8,2),
  union_mem_no BIGINT,
  phone_no BIGINT,
  street VARCHAR(30),
  zip INT,
  state VARCHAR(30),
  city VARCHAR(50)
);

CREATE TABLE Traffic_Controller (
  ssn INT PRIMARY KEY,
  most_recent_exam_date DATE,
  FOREIGN KEY(ssn) REFERENCES Employee(ssn) ON UPDATE CASCADE
);

CREATE TABLE Technician (
  ssn INT PRIMARY KEY,
  FOREIGN KEY(ssn) REFERENCES Employee(ssn) ON UPDATE CASCADE
);

CREATE TABLE Model (
  model_no INT PRIMARY KEY,
  capacity INT,
  weight INT
);

CREATE TABLE Airplane (
  registration_no BIGINT PRIMARY KEY,
  model_no INT,
  FOREIGN KEY(model_no) REFERENCES Model(model_no) ON UPDATE CASCADE
);

CREATE TABLE Expertise_In (
  ssn INT,
  model_no INT,
  PRIMARY KEY(ssn, model_no),
  FOREIGN KEY(ssn) REFERENCES Employee(ssn) ON UPDATE CASCADE,
  FOREIGN KEY(model_no) REFERENCES Model(model_no) ON UPDATE CASCADE
);

CREATE TABLE Test_Info (
  test_no INT PRIMARY KEY,
  name VARCHAR(30),
  max_score INT
);

CREATE TABLE Test (
  test_no INT,
  registration_no BIGINT,
  ssn INT,
  date DATE,
  no_hours INT,
  score DECIMAL(5,2),
  PRIMARY KEY(test_no, registration_no, date),
  FOREIGN KEY(test_no) REFERENCES Test_Info(test_no) ON UPDATE CASCADE,
  FOREIGN KEY(registration_no) REFERENCES Airplane(registration_no) ON UPDATE CASCADE,
  FOREIGN KEY(ssn) REFERENCES Technician(ssn) ON UPDATE CASCADE
);