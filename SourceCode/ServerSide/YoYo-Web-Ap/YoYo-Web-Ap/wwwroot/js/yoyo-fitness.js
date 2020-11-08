$(document).ready(function () {
    var progressBarWidth = 0;
    var progressBarUpdateInterval;
    var athletes = {
        atheleteDtos: [], isWarning: false
    };
    atheleteResult = {};
   // getAtheleteDetails();
    var fitnessBeepData = {};
    bindEvents();
    hideAtheleteDiv();
    hideAtheleteResult();
    hideRunningAlert();
    var startTime = "00:00";
    var isAllAtheteCompleted=false; 

    function bindEvents() {
        $(".glyphicon.glyphicon-play-circle").click(function () {
            hideStartButton();
            getFinessDetailsByStartTimeViaApi('00:00');
            showAthletesDetails();
        });
        $(".btn-primary").click(function (element) {
            element.currentTarget.classList.add('disabled');
            var atheleteId = element.currentTarget.parentNode.parentElement.getAttribute('data-id');
            updateAtheleteRunningStatus(atheleteId, 2);
        });
        $('.btn.btn-danger').click(function (element) {
            var atheleteId = element.currentTarget.parentNode.parentElement.getAttribute('data-id');
            updateAtheleteRunningStatus(atheleteId, 1);
            showResult(element, atheleteId);
        });
    }

    function populateAthletesDetails() {
        var atheleteDivModel = `<div class="row"  data-id={athlete-id}>
                    <div class="col-sm-3"><h4>{athlete-name}</h4></div>
                    <div class="col-sm-3"><button type="button" class="btn btn-primary {disableStatus}">Warning</button></div>
                    <div class="col-sm-3"><button type="button" class="btn btn-danger">Error</button></div>
                    <div><h4 id="result"> </div>
                </div><hr>`;

        $.each(athletes.atheleteDtos, function (index, athlete) {
            var atheleteResolveModel = atheleteDivModel.replace('{athlete-name}', athlete.name);
            atheleteResolveModel = atheleteResolveModel.replace('{athlete-id}', athlete.id);
            if (athlete.isWarning) {
                atheleteResolveModel = atheleteResolveModel.replace('{disableStatus}', 'disabled');
            }
            $('#athlete-details').append(atheleteResolveModel);
        });
    }
     function showResult(element, atheleteId) {
        checkAllAtheleteFinished();
        
        if (isAllAtheteCompleted) {
            showAtheleteResult();
            $('.result-wrapper').hide();
            $('#progress-container').hide();
        } else {
            getAtheleteDetailsById(atheleteId);
            var result = "Result : " + atheleteResult.resultDto.speedLevel + " : " + atheleteResult.resultDto.shuttleNo;
            $(element.currentTarget.parentElement.parentElement).find("#result").text(result);
            $(element.currentTarget.parentElement.parentElement).find('button').remove();
        }
    }

    function updateProgressBar(width) {
        progressBarWidth = Math.round((parseInt(width) / 3640) * 100);
        if (progressBarWidth == 100) {
            clearInterval(progressBarUpdateInterval);
            disableStartButton();
        }
        $('.progress-bar').css('width', progressBarWidth + '%');
        $('.progress-bar').text(progressBarWidth + '%');

    }

    function hideAtheleteDiv() {
        $('#athlete-details').hide();
    }
    function hideAtheleteResult() {
        $('#athlete-results').hide();
    }
    function showAtheleteResult() {
        $('#athlete-results').show();
    }
    function showAthletesDetails() {
        $('#athlete-details').show();
    }

    function disableStartButton() {
        $('.glyphicon.glyphicon-play-circle').attr('disabled', 'disabled');
    }

    function hideStartButton() {
        $('.glyphicon.glyphicon-play-circle').hide();
    }

    function hideRunningAlert() {
        $('.running-alert').hide();
    }

    function showRunningAlert() {
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
        updateProgressBar(parseInt(fitnessRatingData.accumulatedShuttleDistance) - 40);
        setTimeout(getFinessDetailsByStartTimeViaApi, getTimeout(fitnessRatingData));
    }

    function getTimeout(fitnessRatingData) {
        var cumulativeTimeSeconds = parseInt(fitnessRatingData.commulativeTime.split(":")[1]);
        var startTimeSeconds = parseInt(fitnessRatingData.startTime.split(":")[1]);
        var timeout = 0;
        if (cumulativeTimeSeconds > startTimeSeconds) {
            timeout = cumulativeTimeSeconds - startTimeSeconds;
        } else {
            timeout = cumulativeTimeSeconds + 60 - startTimeSeconds;
        }
        console.log(timeout);
        return timeout * 1000;
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
                fitnessBeepData = result;
                pouplateRunningAlertData(result);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
    function getAtheleteDetails() {
        $.ajax({
            url: 'https://localhost:44397/api/fitnessRating/get-athelete-details',
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
               athletes = result;
                populateAthletesDetails();
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function checkAllAtheleteFinished() {
        $.ajax({
            url: 'https://localhost:44397/api/fitnessRating/check-all-athelete-completed',
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                isAllAtheteCompleted = result;
                populateAthletesDetails();
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    function getAtheleteDetailsById(atheleteId) {
        $.ajax({
            url: 'https://localhost:44397/api/fitnessRating/get-athelete-ById?id=' + atheleteId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (result) {
                atheleteResult= result;
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
    function updateAtheleteRunningStatus(id, mode) {
        var atheleteAlertViewModel = { "Id": parseInt(id), "ErrorOrWarn": mode, "TotalDistance": parseInt(fitnessBeepData.accumulatedShuttleDistance)-40 };
        $.ajax({
            url: 'https://localhost:44397/api/fitnessRating/change-athelete-status',
            type: 'POST',
            data: JSON.stringify(atheleteAlertViewModel),
            contentType: "application/json; charset=utf-8",
            dataType:"json",
            success: function (result) {
                console.log(result);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
});