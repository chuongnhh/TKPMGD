// Write your JavaScript code.

//$('#my-start-record-btn').on('click', function (e) {
//    var result = $('#my-result');
//    if (noteContent.length) {
//        noteContent += ' ';
//    }
//    if ($(this).html() == 'Start Recognition') {
//        recognition.stop();
//        $(this).html('Stop Recognition');
//        instructions.text('Voice recognition paused.');
//    } else {
//        recognition.start();
//        $(this).html('Start Recognition');
//        instructions.text('Voice recognition paused.');
//    }
//});

//try {
//    var SpeechRecognition = window.SpeechRecognition || window.webkitSpeechRecognition;
//    var recognition = new SpeechRecognition();
//}
//catch (e) {
//    console.error(e);
//    $('.no-browser-support').show();
//    $('.app').hide();
//}


//// This block is called every time the Speech APi captures a line. 
//recognition.onresult = function (event) {

//    // event is a SpeechRecognitionEvent object.
//    // It holds all the lines we have captured so far. 
//    // We only need the current one.
//    var current = event.resultIndex;

//    // Get a transcript of what was said.
//    var transcript = event.results[current][0].transcript;

//    // Add the current transcript to the contents of our Note.
//    // There is a weird bug on mobile, where everything is repeated twice.
//    // There is no official solution so far so we have to handle an edge case.
//    var mobileRepeatBug = (current == 1 && transcript == event.results[0][0].transcript);

//    if (!mobileRepeatBug) {
//        $('#my-result').html(transcript);
//    }
//};