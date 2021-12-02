SELECT person.id, book.name, author.first_name, author.last_name, author.middle_name,  genre.genre_name
FROM library_card
INNER JOIN book ON library_card.book_id = book.id
INNER JOIN author ON author.id = book.author_id
INNER JOIN book_genre ON book_genre.book_id = book.id
INNER JOIN genre ON genre.id = book_genre.genre_id
INNER JOIN person ON library_card.person_id = person.id
WHERE library_card.person_id = 5;
