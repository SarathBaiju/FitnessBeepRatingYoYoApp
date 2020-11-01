console.log('yoyo-fitness');
$(document).ready(function () {
    var progressBarWidth=0;
    var progressBarUpdateInterval;
    bindEvents();
    hideRunningAlert();
    var startTime = "00:00";

    function bindEvents() {
      $(".glyphicon.glyphicon-play-circle").click(function () {
          hideStartButton();
          getFinessDetailsByStartTimeViaApi('00:00');
        //showRunningAlert();
      //  progressBarUpdateInterval = setInterval(() => {
      //      updateProgressBar();
      //    }, 100);
      });
    }

    function updateProgressBar(width) {
        progressBarWidth = Math.round((parseInt(width) / 3640) * 100);
      if(progressBarWidth==100){
          clearInterval(progressBarUpdateInterval);
          disableStartButton();
      }
      $('.progress-bar').css('width',progressBarWidth+'%');
      $('.progress-bar').text(progressBarWidth+'%');
      
    }
    
    function disableStartButton(){
      $('.glyphicon.glyphicon-play-circle').attr('disabled','disabled');
    }

    function hideStartButton(){
        $('.glyphicon.glyphicon-play-circle').hide();
    }

    function hideRunningAlert(){
        $('.running-alert').hide();
    }

    function showRunningAlert(){
        $('.running-alert').show();
    }

    function pouplateRunningAlertData(fitnessRatingData) {
        $("#speedLevel").text(fitnessRatingData.speedLevel);
        $("#shuttle").text(fitnessRatingData.shuttleNo);
        $("#levelSpeed").text(fitnessRatingData.speed);
        $("#nextShuttleTime").text(getNextShuttleTime(fitnessRatingData.commulativeTime));
        $("#totalTime").text(fitnessRatingData.startTime);
        $("#TotalDistance").text(getTotalDistance(fitnessRatingData.accumulatedShuttleDistance));
        showRunningAlert();
        startTime = getNextShuttleTime(fitnessRatingData.commulativeTime);
        updateProgressBar(parseInt(fitnessRatingData.accumulatedShuttleDistance)-40);
        setTimeout(getFinessDetailsByStartTimeViaApi, getTimeout(fitnessRatingData.commulativeTime));
    }

    function getTimeout(commulativeTime) {
        var timeSplit = commulativeTime.split(":");
        var timeout = parseInt(timeSplit[1]) * 1000;
        return timeout;
    }

    function getTotalDistance(accumulatedShuttleDistance) {
        return parseInt(accumulatedShuttleDistance) - 40;
    }

    function getNextShuttleTime(commulativeTime) {
        var nextShuttleTime = "";
        var splitTime = commulativeTime.split(":"); //here both minute and second will split
        var second = splitTime[1];
        var splitSecond = second.split("");
        if (splitSecond[0] == "0") {
            if (parseInt(splitSecond[1]) >= 9) {
                splitTime[1] = 10;
            }
            else {
                splitSecond[1] = parseInt(splitSecond[1]) + 1;
                second = splitSecond.join("");
                splitTime[1] = second;
            }
            nextShuttleTime = splitTime.join(":");
        } else {
            splitTime[1] = parseInt(splitTime[1]) + 1; // second handling
             nextShuttleTime = splitTime.join(":");
        }
        return nextShuttleTime;
    }

    function getFinessDetailsByStartTimeViaApi() {
        $.ajax({
            url: 'https://localhost:44397/api/fitnessRating/get-ByStartTime?starttime=' + startTime,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                console.log({ result });
                pouplateRunningAlertData(result);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
  });