drop database if exists cse520S;
create database cse520S;
use cse520S;

drop table if exists sensor;
create table sensor (
  id int unsigned not null auto_increment primary key,
  condition varchar(32) not null,
  light varchar(16) not null,
  height varchar(16) default '0',
  humidity varchar(16) not null,
  pressure varchar(16) not null,
  temperature varchar(16) not null,
  lat double not null,
  lon double not null,
  time timestamp not null default now()
) ENGINE = MYISAM;

insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'pressure', 'light', 'humidity', 'pressure', 'temperture', 23.1234, -23.4321);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'pressure', 'light', 'humidity', 'pressure', 'temperture', 23.234, -23.432);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'pressure', 'light', 'humidity', 'pressure', 'temperture', 23.34, -23.43);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'pressure', 'light', 'humidity', 'pressure', 'temperture', 23.4, -23.4);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'pressure', 'light', 'humidity', 'pressure', 'temperture', 23.0, -23.0);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'pressure', 'light', 'humidity', 'pressure', 'temperture', 23.234, -23.4321);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'pressure', 'light', 'humidity', 'pressure', 'temperture', 23.34, -23.432);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'pressure', 'light', 'humidity', 'pressure', 'temperture', 23.4, -23.43);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'pressure', 'light', 'humidity', 'pressure', 'temperture', 23.5, -23.4);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'pressure', 'light', 'humidity', 'pressure', 'temperture', 23.10, -23.0);

insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'height', 'light', 'humidity', 'pressure', 'temperture', 23.51234, -23.4321);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'height', 'light', 'humidity', 'pressure', 'temperture', 23.5234, -23.432);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'height', 'light', 'humidity', 'pressure', 'temperture', 23.534, -23.43);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'height', 'light', 'humidity', 'pressure', 'temperture', 23.54, -23.4);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'height', 'light', 'humidity', 'pressure', 'temperture', 23.50, -23.0);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'height', 'light', 'humidity', 'pressure', 'temperture', 23.5234, -23.4321);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'height', 'light', 'humidity', 'pressure', 'temperture', 23.534, -23.432);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'height', 'light', 'humidity', 'pressure', 'temperture', 23.54, -23.43);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'height', 'light', 'humidity', 'pressure', 'temperture', 23.551, -23.4);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'height', 'light', 'humidity', 'pressure', 'temperture', 23.510, -23.0);

insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'light', 'light', 'humidity', 'pressure', 'temperture', 23.5234, -21.4321);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'light', 'light', 'humidity', 'pressure', 'temperture', 23.634, -21.432);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'light', 'light', 'humidity', 'pressure', 'temperture', 23.84, -21.43);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'light', 'light', 'humidity', 'pressure', 'temperture', 23.94, -21.4);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'light', 'light', 'humidity', 'pressure', 'temperture', 23.10, -21.0);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'light', 'light', 'humidity', 'pressure', 'temperture', 23.2234, -21.4321);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'light', 'light', 'humidity', 'pressure', 'temperture', 23.334, -21.432);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'light', 'light', 'humidity', 'pressure', 'temperture', 23.44, -21.43);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'light', 'light', 'humidity', 'pressure', 'temperture', 23.55, -21.4);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'light', 'light', 'humidity', 'pressure', 'temperture', 23.610, -21.0);

insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'heat', 'light', 'humidity', 'pressure', 'temperture', 23.31234, -22.4321);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'heat', 'light', 'humidity', 'pressure', 'temperture', 23.4234, -22.432);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'heat', 'light', 'humidity', 'pressure', 'temperture', 23.234, -22.43);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'heat', 'light', 'humidity', 'pressure', 'temperture', 23.14, -22.4);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'heat', 'light', 'humidity', 'pressure', 'temperture', 23.50, -22.0);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'heat', 'light', 'humidity', 'pressure', 'temperture', 23.2334, -22.4321);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'heat', 'light', 'humidity', 'pressure', 'temperture', 23.4334, -22.432);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'heat', 'light', 'humidity', 'pressure', 'temperture', 23.23344, -22.43);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'heat', 'light', 'humidity', 'pressure', 'temperture', 23.235, -22.4);
insert into sensor (condition, light, humidity, pressure, temperture, lat, lon) values ( 'heat', 'light', 'humidity', 'pressure', 'temperture', 23.2310, -22.0);
