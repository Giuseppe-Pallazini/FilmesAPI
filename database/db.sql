
DROP DATABASE apiDB;
CREATE DATABASE apiDB;
USE apiDB;

CREATE TABLE tb_filme(
	id_filme		int primary key auto_increment,
    nm_filme		varchar(100),
    ds_genero		varchar(100),
    nr_duracao		int,
    vl_avaliacao	decimal(10,2),
    bt_disponivel	bool,
    dt_lancamento	date
);

create table tb_diretor(
	id_diretor		int primary key auto_increment,
    nm_diretor 		varchar(100),
    dt_nascimento	date,
    id_filme		int,
    foreign key (id_filme) references tb_filme (id_filme)
);


create table tb_ator (
	id_ator			int primary key auto_increment,
    nm_ator			varchar(100),
    vl_altura		decimal(10,2),
    dt_nascimento	date
);


create table tb_filme_ator (
	id_filme_ator 	int primary key auto_increment,
    nm_personagem 	varchar(100),
    id_filme		int,
    id_ator			int,
    foreign key (id_filme) references tb_filme (id_filme),
    foreign key (id_ator)  references tb_ator (id_ator)
);



INSERT INTO tb_filme (nm_filme, ds_genero, nr_duracao, vl_avaliacao, bt_disponivel, dt_lancamento) VALUES ('Avengers: Ultimate', 'Ação', 130, 9.60, 1, '2019-04-25');
INSERT INTO tb_filme (nm_filme, ds_genero, nr_duracao, vl_avaliacao, bt_disponivel, dt_lancamento) VALUES ('Capitão América', 'Ação', 125, 8.70, 1, '2011-07-29');
INSERT INTO tb_filme (nm_filme, ds_genero, nr_duracao, vl_avaliacao, bt_disponivel, dt_lancamento) VALUES ('Homem de Ferro', 'Ação', 143, 9.20, 1, '2008-04-30');
INSERT INTO tb_filme (nm_filme, ds_genero, nr_duracao, vl_avaliacao, bt_disponivel, dt_lancamento) VALUES ('Tróia', 'Ação', 122, 8.30, 1, '2004-05-14');
INSERT INTO tb_filme (nm_filme, ds_genero, nr_duracao, vl_avaliacao, bt_disponivel, dt_lancamento) VALUES ('O hobbit', 'Aventura', 189, 9.10, 1, '2012-12-14');
INSERT INTO tb_filme (nm_filme, ds_genero, nr_duracao, vl_avaliacao, bt_disponivel, dt_lancamento) VALUES ('Frozen', 'Aventura', 132, 7.00, 1, '2014-01-03');
INSERT INTO tb_filme (nm_filme, ds_genero, nr_duracao, vl_avaliacao, bt_disponivel, dt_lancamento) VALUES ('Harry Potter e a Pedra Filosofal', 'Aventura', 170, 6.60, 1, '2011-01-23');
INSERT INTO tb_filme (nm_filme, ds_genero, nr_duracao, vl_avaliacao, bt_disponivel, dt_lancamento) VALUES ('Harry Potter e a Câmara Secreta', 'Aventura', 178, 7.10, 1, '2002-11-22');
INSERT INTO tb_filme (nm_filme, ds_genero, nr_duracao, vl_avaliacao, bt_disponivel, dt_lancamento) VALUES ('Harry Potter e o Cálice de Fogo', 'Aventura', 180, 7.80, 1, '2005-11-25');


INSERT INTO tb_ator (nm_ator, vl_altura, dt_nascimento) VALUES ('Robert D.', '1.75', '1970-05-29'); 
INSERT INTO tb_ator (nm_ator, vl_altura, dt_nascimento) VALUES ('Scarlett J.', '1.6', '1984-11-22'); 
INSERT INTO tb_ator (nm_ator, vl_altura, dt_nascimento) VALUES ('Chris Evans', '1.83', '1981-6-13'); 
INSERT INTO tb_ator (nm_ator, vl_altura, dt_nascimento) VALUES ('Sebastian Stan', '1.83', '1982-08-13'); 
INSERT INTO tb_ator (nm_ator, vl_altura, dt_nascimento) VALUES ('Orlando Bloom', '1.8', '1977-1-13'); 
INSERT INTO tb_ator (nm_ator, vl_altura, dt_nascimento) VALUES ('Ian McKellen', '1.8', '1939-05-25'); 
INSERT INTO tb_ator (nm_ator, vl_altura, dt_nascimento) VALUES ('Daniel Radcliffe', '1.65', '1989-07-23'); 
INSERT INTO tb_ator (nm_ator, vl_altura, dt_nascimento) VALUES ('Emma Watson', '1.63', '1990-04-15'); 
INSERT INTO tb_ator (nm_ator, vl_altura, dt_nascimento) VALUES ('Brad Pitt', '1.8', '1963-12-18');


INSERT INTO tb_diretor (nm_diretor, dt_nascimento, id_filme) VALUES ('David Yates', '1963-10-8', 7);
INSERT INTO tb_diretor (nm_diretor, dt_nascimento, id_filme) VALUES ('Alfonso Cuarón', '1961-11-22', 8);
INSERT INTO tb_diretor (nm_diretor, dt_nascimento, id_filme) VALUES ('Chris Columbus', '1958-09-10', 9);
INSERT INTO tb_diretor (nm_diretor, dt_nascimento, id_filme) VALUES ('Joss Whedon', '1954-08-15', 1);
INSERT INTO tb_diretor (nm_diretor, dt_nascimento, id_filme) VALUES ('Joe Johnston', '1956-04-05', 2);
INSERT INTO tb_diretor (nm_diretor, dt_nascimento, id_filme) VALUES ('Jon Favreau', '1951-06-22', 3); 
INSERT INTO tb_diretor (nm_diretor, dt_nascimento, id_filme) VALUES ('Wolfgang Petersen', '1962-2-7', 4); 
INSERT INTO tb_diretor (nm_diretor, dt_nascimento, id_filme) VALUES ('Jennifer Lee', '1964-2-2', 6); 
INSERT INTO tb_diretor (nm_diretor, dt_nascimento, id_filme) VALUES ('Peter Jackson', '1954-6-9', 5);


INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Tony S.', '1', '1'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Natasha R.', '1', '2'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Steve R.', '2', '3'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Bucky', '2', '4'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Tony S.', '3', '1'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Aquiles', '4', '9'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Legolas', '5', '5'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Gandalf', '5', '6'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Harry P.', '7', '7'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Hermione', '7', '8'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Harry P.', '8', '7'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Hermione', '8', '8'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Harry P.', '9', '7'); 
INSERT INTO tb_filme_ator (nm_personagem, id_filme, id_ator) VALUES ('Hermione', '9', '8');



SELECT * FROM tb_filme;
SELECT * FROM tb_diretor;
SELECT * FROM tb_ator;
SELECT * FROM tb_filme_ator;

SELECT 	nm_filme,
		nm_diretor,
        nm_ator
	from tb_filme		f
    join tb_diretor		d
	  on f.id_filme		= d.id_filme
	join tb_filme_ator	fa
	  on fa.id_filme	= f.id_filme
	join tb_ator		a
	  on a.id_ator		= fa.id_ator;