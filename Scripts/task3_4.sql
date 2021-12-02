SELECT author.first_name, author.last_name, author.middle_name, genre.genre_name, COUNT(book.name)
FROM author
INNER JOIN book ON book.author_id = author.id
INNER JOIN book_genre ON book_genre.book_id = book.id
INNER JOIN genre ON genre.id = book_genre.genre_id
GROUP BY author.first_name, author.last_name, author.middle_name, genre.genre_name;