use Parcial02;
-- FERNANDOBD\SQLEXPRESS
create table Tipos(

id  int primary key identity(1,1),

nombre  varchar(20) not null,

);

create table Ingresos(

id int primary key identity(1,1),

nombre  varchar(100) not null,

fecha date not null,

valor decimal(8,4) not null,

estado bit not null,

idTipo int not null,

foreign key (idTipo) references Tipos(id)

);

insert into Tipos(nombre) values('sueldo'), ('horas extras'), ('bono')