drop database cse520S;
create database cse520S;
use cse520S;

drop table if exists sensor;
create table sensor (
  id int unsigned not null auto_increment primary key,
  type varchar(16) not null,
  value int not null,
  lat double not null,
  lon double not null,
  time timestamp not null default now()
) ENGINE = MYISAM;

insert into sensor (type, value, lat, lon) values ( 'pressure', 22, 23.1234, -23.4321);
insert into sensor (type, value, lat, lon) values ( 'pressure', 23, 23.234, -23.432);
insert into sensor (type, value, lat, lon) values ( 'pressure', 24, 23.34, -23.43);
insert into sensor (type, value, lat, lon) values ( 'pressure', 25, 23.4, -23.4);
insert into sensor (type, value, lat, lon) values ( 'pressure', 26, 23.0, -23.0);
insert into sensor (type, value, lat, lon) values ( 'pressure', 27, 23.234, -23.4321);
insert into sensor (type, value, lat, lon) values ( 'pressure', 28, 23.34, -23.432);
insert into sensor (type, value, lat, lon) values ( 'pressure', 29, 23.4, -23.43);
insert into sensor (type, value, lat, lon) values ( 'pressure', 30, 23.5, -23.4);
insert into sensor (type, value, lat, lon) values ( 'pressure', 31, 23.10, -23.0);

insert into sensor (type, value, lat, lon) values ( 'height', 22, 23.1234, -23.4321);
insert into sensor (type, value, lat, lon) values ( 'height', 23, 23.234, -23.432);
insert into sensor (type, value, lat, lon) values ( 'height', 24, 23.34, -23.43);
insert into sensor (type, value, lat, lon) values ( 'height', 25, 23.4, -23.4);
insert into sensor (type, value, lat, lon) values ( 'height', 26, 23.0, -23.0);
insert into sensor (type, value, lat, lon) values ( 'height', 27, 23.234, -23.4321);
insert into sensor (type, value, lat, lon) values ( 'height', 28, 23.34, -23.432);
insert into sensor (type, value, lat, lon) values ( 'height', 29, 23.4, -23.43);
insert into sensor (type, value, lat, lon) values ( 'height', 30, 23.5, -23.4);
insert into sensor (type, value, lat, lon) values ( 'height', 31, 23.10, -23.0);

insert into sensor (type, value, lat, lon) values ( 'light', 22, 23.1234, -23.4321);
insert into sensor (type, value, lat, lon) values ( 'light', 23, 23.234, -23.432);
insert into sensor (type, value, lat, lon) values ( 'light', 24, 23.34, -23.43);
insert into sensor (type, value, lat, lon) values ( 'light', 25, 23.4, -23.4);
insert into sensor (type, value, lat, lon) values ( 'light', 26, 23.0, -23.0);
insert into sensor (type, value, lat, lon) values ( 'light', 27, 23.234, -23.4321);
insert into sensor (type, value, lat, lon) values ( 'light', 28, 23.34, -23.432);
insert into sensor (type, value, lat, lon) values ( 'light', 29, 23.4, -23.43);
insert into sensor (type, value, lat, lon) values ( 'light', 30, 23.5, -23.4);
insert into sensor (type, value, lat, lon) values ( 'light', 31, 23.10, -23.0);

insert into sensor (type, value, lat, lon) values ( 'heat', 22, 23.1234, -23.4321);
insert into sensor (type, value, lat, lon) values ( 'heat', 23, 23.234, -23.432);
insert into sensor (type, value, lat, lon) values ( 'heat', 24, 23.34, -23.43);
insert into sensor (type, value, lat, lon) values ( 'heat', 25, 23.4, -23.4);
insert into sensor (type, value, lat, lon) values ( 'heat', 26, 23.0, -23.0);
insert into sensor (type, value, lat, lon) values ( 'heat', 27, 23.234, -23.4321);
insert into sensor (type, value, lat, lon) values ( 'heat', 28, 23.34, -23.432);
insert into sensor (type, value, lat, lon) values ( 'heat', 29, 23.4, -23.43);
insert into sensor (type, value, lat, lon) values ( 'heat', 30, 23.5, -23.4);
insert into sensor (type, value, lat, lon) values ( 'heat', 31, 23.10, -23.0);
