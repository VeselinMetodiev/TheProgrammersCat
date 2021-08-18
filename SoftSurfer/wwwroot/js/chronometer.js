let clock = document.getElementById('clock'),
    startStop = document.getElementById('StartPauseButton'),
    resetButton = document.getElementById('ResetButton'),
    seconds = 0, minutes = 0, hours = 0,
    t;

let on = true;

//Increments the timer and displays the elapsed time
function add() {
    seconds++;
    if (seconds >= 60) {
        seconds = 0;
        minutes++;
        if (minutes >= 60) {
            minutes = 0;
            hours++;
        }
    }

    clock.textContent = (hours ? (hours > 9 ? hours : "0" + hours) : "00") + ":" + (minutes ? (minutes > 9 ? minutes : "0" + minutes) : "00") + ":" + (seconds > 9 ? seconds : "0" + seconds);

    timer();
}

function timer() {
    t = setTimeout(add, 1000);
}


// Starts the chronometer if paused, and pauses the chronometer if counting
startStop.onclick = check;

function check() {
    if (on) {
        timer();
        on = false;
    }
    else {
        stop();
        on = true;
    }
}


function stop() {
    clearTimeout(t);
}

// Reset button
resetButton.onclick = function () {
    if (!on) {
        stop();
        on = true;
    }
    clock.textContent = "00:00:00";
    seconds = 0; minutes = 0; hours = 0;

}