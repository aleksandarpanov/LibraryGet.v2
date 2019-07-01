"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44393/chatHub").build();

connection.on("BookResponse", function (bookName) {
    $("#exampleModalLabel").text('Request for the book approved');
    $("#signalRMessage").text("Your request for the book '" + bookName + "' has been approved'");
    $('#exampleModal').modal('show');
    $('#modalCloseButton').click(function () {
        window.location.href = "https://localhost:44393/Books/Index";
    });
});

connection.on("BookReturned", function (bookName) {
    $("#exampleModalLabel").text('Book return');
    $("#signalRMessage").text("Book'" + bookName + "' is returned to the library.'");
    $('#exampleModal').modal('show');
    $('#modalCloseButton').click(function () {
        window.location.href = "https://localhost:44393/Books/Index";
    });
});

connection.on("ReceiveMessage", function (message) {
    console.log(message);
});

connection.on("BookRequest", function (bookReservation) {
    $("#exampleModalLabel").text('New book request')
    $("#signalRMessage").text("The user" + bookReservation.userName + " has sent a request for the book '" + bookReservation.bookName + "'")
    $('#exampleModal').modal('toggle')
    $('#modalCloseButton').click(function () {
        window.location.href = "https://localhost:44393/BooksReservation/Index";
    });
});

connection.start({ withCredentials: true }).then(function () {
    console.log("connected");
}).catch(function (err) {
    return console.error(err.toString());
});