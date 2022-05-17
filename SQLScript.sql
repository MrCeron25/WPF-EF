/*
use [master];
IF EXISTS (SELECT * FROM SYS.DATABASES WHERE NAME='course_work_WPF_EF')
BEGIN
	DROP DATABASE course_work_WPF_EF;
END
CREATE DATABASE course_work_WPF_EF;
*/

use course_work_WPF_EF;
GO

drop table if exists tickets;
drop table if exists [system];
drop table if exists users;
drop table if exists flights;
drop table if exists archive_flights;
drop table if exists airplane;
drop table if exists cities;
drop table if exists country;


drop table if exists users;
create table users(
	id bigint identity primary key,
	[name] nvarchar(255) not null,
	[surname] nvarchar(255) not null,
	sex char(1)
	check(sex = 'М' or sex = 'Ж'),
	date_of_birth date not null default getdate(),
	passport_series int not null,
	passport_id int not null
);

drop table if exists [system];
create table [system](
	[user_id] bigint primary key,
	[login] nvarchar(255) not null,
	[password] nvarchar(255) not null,
	[is_admin] bit not null default 0,
	FOREIGN KEY ([user_id]) REFERENCES users (id)
);

drop table if exists country;
create table country(
	id bigint identity primary key, 
	[name] nvarchar(255) not null unique,
	index idx_countries_name([name]) -- для быстрого поиска
);

drop table if exists cities;
create table cities(
	id bigint identity primary key,
	[name] nvarchar(255) not null,
	[country_id] bigint not null,
	FOREIGN KEY ([country_id]) REFERENCES country (id),
	index idx_cities_name([name]) -- для быстрого поискаw
);

drop table if exists airplane;
create table airplane(
	id bigint identity primary key,
	[model] nvarchar(255) not null unique,
	number_of_seats int not null,
	index idx_airplanes_model_name([model]) -- для быстрого поиска
);

-- рейсы
drop table if exists flights;
create table flights(
	id bigint identity primary key,
	[flight_name] nvarchar(10) not null,
	[departure_city] bigint not null,
	[arrival_city] bigint not null,
	airplane_id bigint not null,
	[departure_date] datetime not null, 
	[travel_time] time not null,
	[arrival_date] AS DATEADD(MINUTE, DATEDIFF(MINUTE, 0, [travel_time]), [departure_date]),
	[price] float not null,
	[is_archive] bit not null default 0,
	FOREIGN KEY (airplane_id) REFERENCES airplane (id),
	FOREIGN KEY ([departure_city]) REFERENCES cities (id),
	FOREIGN KEY ([arrival_city]) REFERENCES cities (id)
);

drop table if exists tickets;
create table tickets(
	id bigint identity primary key,
	[flight_id] bigint not null,
	seat_number bigint not null,
	[user_id] bigint not null,
	FOREIGN KEY ([flight_id]) REFERENCES flights (id),
	FOREIGN KEY ([user_id]) REFERENCES users (id)
);

insert into users([name],[surname],sex,date_of_birth,passport_id,passport_series) values
('Ярослав', 'Фуфаев', 'м', '1990-03-01', 2265, 665038),
('Акимов', 'Аким', 'м', '1960-02-17', 6186, 788845),
('Пронин', 'Василий', 'м', '1975-07-20', 1489, 686287),
('Петров', 'Пётр', 'м', '1982-01-02', 3325, 281050),
('Петренко', 'Пётр', 'м', '1982-01-02', 1602, 943241),
('Петренко', 'Иван', 'м', '1982-01-02', 7857, 617316),
('Дубова', 'Галина', 'ж', '1960-02-14', 4240, 831619),
('Бабичев', 'Евгений', 'м', '1960-06-12', 2576, 742885),
('Нагаев', 'Дмитрий', 'м', '1985-09-20', 5113, 787175),
('Семеновна', 'Ольга', 'ж', '1960-06-12', 7083, 202561);

insert into [system]([user_id],[login],[password],[is_admin]) values
(1, '1', 'Tf9Oo0DwqCPxXT9PAati6uDl2lecy4Ufjbnf6ExYsrN7iZA6dA4e4XLaeTpuedVg5ff5vQWKEqKAQz7W+kZRCg==', 0),
(2, '2', 'QLJEESZB3XjdT5O2yRkN1G4AmRlNWkQle3761u+f9Gg9oe2gJERIyzQ6poj10+/XMU2v5YCsC8vxFa7Kno3BFA==', 1),
(3, 'user3', 'OrAffdtrcnJWeiwSpT/U0flTCWH6LOmbD71NZX213buPk/7oyvB0nNxd0US8p/NkFtWXkJ1lcf1iX+kwB9uo2Q==', 1),
(4, 'user4', 'ef4daonCm4Q3t/anKH/edMM9Rq6JukUUNq+7iqvczM/pTg/m0hyEHksfgjX+1vlrMssBLc86/SHDD3Oz/IF9Eg==', 1),
(5, 'user5', 'CbQ7x9misSkTRqbiYXzIVqosfU264tk4GXUzj7eFx1AWDf7DOJDd+PUGtkOf0ycvtXIhKCrRmsxy781UBcqbUQ==', 0),
(6, 'user6', 'WMcPCC9UWLnCRWh30HhU6Ng3TrT6L8KhEBTWUH2JBvdxdGdwEKwh6q3DJV577iYOwi/ICMGGiTbfIdiej+ReTQ==', 0),
(7, 'user7', 'dKfuiruH1PsMNMq5omXcM9kvqY0hzqm8evFuWT8T9hEQHaaDEmcbJOI8V/gWlr9a0a5Xxv6HBiOioxkECYYJbw==', 0),
(8, 'user8', 'EVD2JSWN7FZ1eNDy8GynNFbjpYtoRKhta+2RMOpJeYzw9Ts+tC5tweXEpq2etRzUqykAXrixweENVXdKv5qhZw==', 0),
(9, 'user9', 'yx8jfAxcBfLOBeLemRyGZ3UFbbkqAQTw/O6C3mGyq81AS+hQUP3dxftYF3QaNe78Fz/X/KCZo9vx+lbykMhUtA==', 0),
(10, 'user10', 'y5FEQU4mIPXbStdRnnhQwoQ4UEyag1B3ha0nPlzHZ/Y1yHOV4dM55Z/13eMsKt/AUP/PbHjQKGRVG+7csXyUnw==', 0);

/*
(1,'1', '1', 0),
(2,'2', '2', 1),
(3,'user3', '2DI804OKODJB3LI', 1),
(4,'user4', '2D6FLD68SNDKJDI', 1),
(5,'user5', 'DGNDLV25695FNJD', 0),
(6,'user6', 'G2OYD411QO77I02', 0),
(7,'user7', 'CB9B09ZKWLWKOH7', 0),
(8,'user8', 'VV576US7ZYLN8C3', 0),
(9,'user9', '6C4HRWLEG2E178P', 0),
(10,'user10', 'HTOSVWZGTWNYBZO', 0);
*/

insert into country([name]) values
('Россия'),
('Китай'),
('Япония'),
('США'),
('Германия');

insert into cities([name],country_id) values
('Москва',1),
('Калининград',1),
('Пекин',2),
('Тяньцзинь',2),
('Айсай',3),
('Самара',1),
('Нью-Йорк',4),
('Берлин',5),
('Дайсен',3),
('Чунцин',2),
('Мюнхен',5),
('Шанхай',2),
('Нижний Новгород',1),
('Гаосюн',2),
('Тайбэй',2),
('Асахи',3),
('Астрахань',1),
('Детройт',4),
('Чикаго',4),
('Гамбург',5),
('Санкт-Петербург',1);

insert into airplane([model], number_of_seats) values
('Boeing 717', 124),
('Boeing 737',162),
('Boeing 747-400',660),
('Boeing 757-200',200),
('Boeing 757-300',300),
('Boeing-777-300',450),
('Boeing-777-200',300),
('Airbus A310',220),
('Airbus A350-900',310),
('Airbus A350-1000',350);

insert into flights([departure_city], [arrival_city], airplane_id, flight_name, travel_time,price,departure_date) values
(1, 6, 1,'SU 1602','03:05',70000,convert(datetime,'2023-03-05 15:04:30',20)),
(1, 6, 3,'SU 1602','01:45',65000,convert(datetime,'2024-01-09 07:34:29',20)),
(1, 21, 1,'SU 6016','00:49',13000,convert(datetime,'2023-10-26 05:35:33',120)),
(4, 10, 1,'SU 8043','06:00',100000,convert(datetime,'2024-01-22 06:52:49',120)),
(2, 20, 2,'SU 9997','02:08',80000,convert(datetime,'2022-06-25 02:27:56',120)),
(1, 19, 4,'SU 6205','01:45',90000,convert(datetime,'2024-08-13 22:43:46',120)),
(7, 20, 3,'SU 5813','00:34',76000,convert(datetime,'2025-01-08 12:08:42',120)),
(6, 17, 5,'SU 6859','02:25',56000,convert(datetime,'2022-08-04 01:37:21',120)),
(2, 5, 6,'SU 9642','01:16',45000,convert(datetime,'2023-10-23 04:23:49',120)),
(16, 4, 7,'SU 6252','00:20',32000,convert(datetime,'2022-05-09 00:01:16',120)),
(1, 20, 9,'SU 8999','05:11',77000,convert(datetime,'2023-01-22 04:36:17',120)),
(17, 9, 10,'SU 3146','00:42',66000,convert(datetime,'2023-12-07 04:24:49',120)),
(14, 3, 8,'SU 1357','02:32',84000,convert(datetime,'2022-11-08 14:14:22',120)),
(9, 5, 2,'SU 6498','03:22',5000,convert(datetime,'2022-01-01 14:14:22',120)),
(6, 4, 7,'SU 1931','01:55',2000,convert(datetime,'2022-01-01 14:14:22',120)),
(7, 8, 3,'SU 6943','01:45',66000,convert(datetime,'2022-05-07 15:00:00',120));

insert into tickets(flight_id, seat_number,[user_id]) values
(1,1, 10),
(1,14, 5),
(1,15, 6),
(1,20, 4),
(1,7, 1),
(1,18, 4),
(1,16, 6),
(1,19, 6),
(1,17, 5),
(1,6, 9),
(2,9, 4),
(2,16, 7),
(2,11, 2),
(2,6, 2),
(2,18, 10),
(2,17, 9),
(2,20, 5),
(2,3, 5),
(2,19, 6),
(2,13, 5),
(3,1, 9),
(3,2, 4),
(3,10, 7),
(3,17, 8),
(3,8, 7),
(3,12, 6),
(3,20, 4),
(3,13, 5),
(3,3, 1),
(3,4, 8),
(4,19, 6),
(4,14, 1),
(4,1, 8),
(4,12, 8),
(4,3, 1),
(4,16, 10),
(4,15, 8),
(4,7, 2),
(4,9, 5),
(4,18, 7),
(5,19, 10),
(5,18, 7),
(5,2, 6),
(5,7, 1),
(5,6, 7),
(5,11, 9),
(5,12, 3),
(5,20, 5),
(5,1, 2),
(5,4, 2),
(6,12, 4),
(6,15, 7),
(6,13, 8),
(6,11, 9),
(6,14, 5),
(6,3, 1),
(6,5, 1),
(6,4, 8),
(6,10, 5),
(6,18, 2),
(7,9, 6),
(7,17, 6),
(7,8, 6),
(7,4, 4),
(7,13, 3),
(7,7, 4),
(7,20, 2),
(7,10, 3),
(7,2, 6),
(7,15, 7),
(8,2, 6),
(8,5, 7),
(8,18, 8),
(8,16, 9),
(8,3, 4),
(8,19, 8),
(8,10, 3),
(8,1, 8),
(8,6, 3),
(8,7, 3),
(9,11, 8),
(9,3, 6),
(9,18, 5),
(9,15, 7),
(9,12, 4),
(9,16, 9),
(9,13, 6),
(9,14, 9),
(9,9, 10),
(9,17, 6),
(10,11, 7),
(10,8, 2),
(10,15, 3),
(10,12, 9),
(10,7, 9),
(10,2, 6),
(10,5, 4),
(10,20, 8),
(10,3, 2),
(10,13, 3),
(14,11, 7),
(14,8, 6),
(14,15, 3);

/*
GO
IF EXISTS (SELECT name FROM sysobjects WHERE name = 'GetStatisticsOnSoldTickets' AND type = 'P')
BEGIN
   DROP PROCEDURE GetStatisticsOnSoldTickets;
END
GO

GO
CREATE PROCEDURE GetStatisticsOnSoldTickets
AS
BEGIN
	select 
		year(fl.departure_date) [Год],
		DATENAME(month, fl.departure_date) [Месяц],
		sum(fl.price) [Сумма по всем билетам]
	from tickets ti
	join flights fl on fl.id = ti.flight_id
	group by fl.departure_date
	order by fl.departure_date
END;
GO
*/

/*
select * from users
select * from system
select * from tickets
select * from country
select * from airplane
select * from cities

select * from flights
where flight_name = 'SU 8043'
*/
