create database examapp;

use examapp;


create table classes
(
id int primary key identity,
class_number tinyint UNIQUE  
)


create table teachers
(
id int primary key identity,
teacher_name nvarchar(30) not null,
teacher_surname nvarchar(30) not null,
)


create table subjects
(
id int primary key identity,
code char(3) not null UNIQUE ,
subject_name nvarchar(30) not null,
class_id int foreign key references classes(id),
teacher_id int foreign key references teachers(id)


)
-- 1 Aytac 1 
create table students
(
id int primary key identity,
student_number int not null UNIQUE,
student_name nvarchar(30) not null,
student_surname nvarchar(30) not null,
class_id int not null foreign key references classes(id),

)


create table exams
(
id int primary key identity,
subject_id int not null foreign key references subjects(id),
student_id int not null foreign key references students(id),
exam_date date not null,
score tinyint not null  CHECK (score BETWEEN 0 AND 10)
)



INSERT INTO classes (class_number) VALUES
(1),
(2),
(3),
(4),
(5),
(6),
(7),
(8),
(9),
(10),
(11);

INSERT INTO teachers (teacher_name, teacher_surname) VALUES
(N'Aytac', N'Qələndərova'),
(N'Ayaz', N'Abdullayev'),
(N'Ayxan', N'Quliyev');



INSERT INTO subjects (code, subject_name, class_id, teacher_id) VALUES
('MAT', N'Riyaziyyat', 1, 1),
('FIZ', N'Fizika', 2, 2),
('KMY', N'Kimya', 3, 3);



INSERT INTO students (student_number, student_name, student_surname, class_id) VALUES
(10001, N'Kamran', N'Kərimov', 1),
(10002, N'Ləman', N'Həsənova', 1),

(10003, N'Ramil', N'Hüseynov', 2),
(10004, N'Günel', N'Əliyev', 2),

(10005, N'Nicat', N'İsmayılov', 3),
(10006, N'Günay', N'Rzayeva', 3);


INSERT INTO exams (subject_id, student_id, exam_date, score) VALUES
(1, 1, '2025-05-10', 9),
(1, 2, '2025-05-10', 8),

(2, 3, '2025-05-12', 7),
(2, 4, '2025-05-12', 6),

(3, 5, '2025-05-15', 10),
(3, 6, '2025-05-15', 9);

select * from subjects;
select * from students;
select * from exams;
select * from classes;
