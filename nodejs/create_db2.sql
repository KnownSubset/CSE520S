drop database if exists cse520S;
create database cse520S;
use cse520S;

drop table if exists sensor;
create table sensor (
  id int unsigned not null auto_increment primary key,
  type varchar(16) not null,
  value varchar(32) not null,
  lat double not null,
  lon double not null,
  time timestamp not null default now()
) ENGINE = MYISAM;

insert into sensor (type, value, lat, lon) values ( 'pressure', 22, 38.648493,-90.309212);
insert into sensor (type, value, lat, lon) values ( 'pressure', 23, 38.643493,-90.309292);
insert into sensor (type, value, lat, lon) values ( 'pressure', 24, 38.649493,-90.309202);
insert into sensor (type, value, lat, lon) values ( 'pressure', 25, 38.647493,-90.308232);
insert into sensor (type, value, lat, lon) values ( 'pressure', 26, 38.618493,-90.300232);
insert into sensor (type, value, lat, lon) values ( 'pressure', 27, 38.698493,-90.301232);
insert into sensor (type, value, lat, lon) values ( 'pressure', 28, 38.628493,-90.309832);
insert into sensor (type, value, lat, lon) values ( 'pressure', 29, 38.648593,-90.309732);
insert into sensor (type, value, lat, lon) values ( 'pressure', 30, 38.648193,-90.309332);
insert into sensor (type, value, lat, lon) values ( 'pressure', 31, 38.648793,-90.309232);

insert into sensor (type, value, lat, lon) values ( 'humid', 22, 38.648793,-90.301232);
insert into sensor (type, value, lat, lon) values ( 'humid', 23, 38.648093,-90.345232);
insert into sensor (type, value, lat, lon) values ( 'humid', 24, 38.648193,-90.302232);
insert into sensor (type, value, lat, lon) values ( 'humid', 25, 38.613493,-90.301132);
insert into sensor (type, value, lat, lon) values ( 'humid', 26, 38.617493,-90.300032);
insert into sensor (type, value, lat, lon) values ( 'humid', 27, 38.629493,-90.320232);
insert into sensor (type, value, lat, lon) values ( 'humid', 28, 38.648773,-90.318232);
insert into sensor (type, value, lat, lon) values ( 'humid', 29, 38.646593,-90.310232);
insert into sensor (type, value, lat, lon) values ( 'humid', 30, 38.644293,-90.309112);
insert into sensor (type, value, lat, lon) values ( 'humid', 31, 38.641993,-90.309162);

insert into sensor (type, value, lat, lon) values ( 'heat', 22, 38.473493,-90.325232);
insert into sensor (type, value, lat, lon) values ( 'heat', 23, 38.643493,-90.333232);
insert into sensor (type, value, lat, lon) values ( 'heat', 24, 38.646393,-90.306632);
insert into sensor (type, value, lat, lon) values ( 'heat', 25, 38.642493,-90.301732);
insert into sensor (type, value, lat, lon) values ( 'heat', 26, 38.647493,-90.301132);
insert into sensor (type, value, lat, lon) values ( 'heat', 27, 38.684493,-90.309102);
insert into sensor (type, value, lat, lon) values ( 'heat', 28, 38.669493,-90.309112);
insert into sensor (type, value, lat, lon) values ( 'heat', 29, 38.610493,-90.30132);
insert into sensor (type, value, lat, lon) values ( 'heat', 30, 38.676493,-90.32232);
insert into sensor (type, value, lat, lon) values ( 'heat', 31, 38.64393,-90.30942);

insert into sensor (type, value, lat, lon) values ( 'light','4.81219-171.52365_th', 4.81219,-171.52365);
insert into sensor (type, value, lat, lon) values ( 'light','18.44288-28.68024_th', 18.44288,-28.68024);
insert into sensor (type, value, lat, lon) values ( 'light','38.66172-90.32312_th', 38.66172,-90.32312);
insert into sensor (type, value, lat, lon) values ( 'light','38.66173-90.32319_th', 38.66173,-90.32319);
