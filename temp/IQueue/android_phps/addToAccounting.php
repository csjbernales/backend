<?php
 if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $id = $_POST['id'];
    $datacode = $_POST['datacode'];
    $firebaseKey = $_POST['fireID'];
    $firstname = $_POST['firstname'];
    $middlename = $_POST['middlename'];
    $lastname = $_POST['lastname'];
    $top = $_POST['top'];
    $course = $_POST['course'];
    $current_term = $_POST['current_term'];
    $contactnum = $_POST['contactnum'];
    $status = "Pending";
    $unix = time();
    
    define('HOST','localhost');
    define('USER','foobar2018');
    define('PASS','foobar2018');
    define('DB','thesis_db');
    
    $con = mysqli_connect(HOST,USER,PASS,DB) or die('Unable to Connect');
    
        
        $sql = "INSERT INTO ao_transactions (stud_num, data_key, queue_code, datetime, firstname, lastname, program, purp, term, status, contact_num) VALUES ('$id', '$datacode', '$firebaseKey', '$unix', '$lastname', '$current_term', '$middlename', '$course', '$top', '$status', '$contactnum')";
        
        
        
        if (mysqli_query($con, $sql)) {
            echo 'Queue added successfully';
        } else {
            echo 'Please try again.';
        }
        echo " " . $id;
        mysqli_close($con);
 } else {
     echo 'error';
 }
?>