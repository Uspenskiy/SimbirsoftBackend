CREATE TABLE genre
(
	id SERIAL PRIMARY KEY,
	genre_name VARCHAR(50) NOT NULL
);

CREATE TABLE author
(
	id SERIAL PRIMARY KEY,
	first_name VARCHAR(50) NOT NULL,
	last_name VARCHAR(50) NOT NULL,
	middle_name VARCHAR(50)
);

CREATE TABLE person
(
	id SERIAL PRIMARY KEY,
	birth_date timestamp with time zone,
	first_name VARCHAR(50) NOT NULL,
	last_name VARCHAR(50) NOT NULL,
	middle_name VARCHAR(50)
);

CREATE TABLE book
(
	id SERIAL PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	author_id INTEGER REFERENCES author (Id)
);

CREATE TABLE book_genre
(
	book_id INTEGER REFERENCES book (Id),
	genre_id INTEGER REFERENCES genre (Id),
	CONSTRAINT book_genre_pkey PRIMARY KEY (book_id, genre_id) 
);

CREATE TABLE library_card
(
	book_id INTEGER REFERENCES book (Id),
	person_id INTEGER REFERENCES person (Id),
	CONSTRAINT library_card_pkey PRIMARY KEY (book_id, person_id) 
);
