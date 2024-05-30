const connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7061/chat-hub")
  .configureLogging(signalR.LogLevel.Information)
  .build();

connection.on("ReceiveMessage", function (message) {
  console.log("Received message: ", message);
  const li = document.createElement("li");
  li.textContent = message;
  document.getElementById("messagesList").appendChild(li);
});

connection
  .start()
  .then(function () {
    console.log("Connected to SignalR hub");
    document
      .getElementById("sendButton")
      .addEventListener("click", function (event) {
        const message = document.getElementById("messageInput").value;
        fetch(`https://localhost:7061/api/Values?message=${message}`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ message: message }),
        })
          .then((response) => {
            if (response.ok) {
              console.log("Message sent to server");
            } else {
              console.error("Failed to send message to server");
            }
          })
          .catch((error) => {
            console.error("Error sending message to server: ", error);
          });
        event.preventDefault();
      });
  })
  .catch(function (err) {
    console.error(err.toString());
  });
