console.log('yoyo-fitness');
$(document).ready(function () {
    var progressBarWidth=0;
    var progressBarUpdateInterval;
    bindEvents();
    hideRunningAlert();
    function bindEvents() {
      $(".glyphicon.glyphicon-play-circle").click(function () {
        hideStartButton();
        showRunningAlert();
        progressBarUpdateInterval = setInterval(() => {
            updateProgressBar();
          }, 100);
      });
    }
    function updateProgressBar() {
      progressBarWidth += 1;
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
  });