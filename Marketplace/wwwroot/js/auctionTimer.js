var timeLeft = 600; // 10 minutes in seconds

function updateTimer() {
    var minutes = Math.floor(timeLeft / 60);
    var seconds = timeLeft % 60;
    document.getElementById("timer").innerHTML = minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
    if (timeLeft > 0) {
        timeLeft--;
        setTimeout(updateTimer, 1000);
    }
}

updateTimer();