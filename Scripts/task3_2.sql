SELECT author.first_name, author.middle_name, author.last_name,  
book.name, genre.genre_name
FROM author
LEFT JOIN book ON book.author_id = author.id
INNER JOIN book_genre ON book_genre.book_id = book.id
INNER JOIN genre ON genre.id = book_genre.genre_id
WHERE author.id = 2;