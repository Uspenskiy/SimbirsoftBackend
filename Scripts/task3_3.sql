SELECT genre.genre_name, COUNT(book.name)
FROM book_genre
INNER JOIN book ON book_genre.book_id = book.id
INNER JOIN genre ON book_genre.genre_id = genre.id
GROUP BY genre.genre_name;