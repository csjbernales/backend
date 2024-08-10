<?php
 if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $id = $_POST['id'];
    $fireID = $_POST['fireID'];
    $id123 = $_POST['id123'];
    $lcourse = $_POST['lcourse'];
    $firstname = $_POST['firstname'];
    $middlename = $_POST['middlename'];
    $lastname = $_POST['lastname'];
    $top = $_POST['top'];
    $gcurrent_sy= $_POST['gcurrent_sy'];
    $gcurrent_term = $_POST['gcurrent_term'];
    $transID = $_POST['transID'];
    $contactnum = $_POST['contactnum'];
    $status = "Pending";
    $unix = time();
    
    define('HOST','localhost');
    define('USER','foobar2018');
    define('PASS','foobar2018');
    define('DB','thesis_db');
    
    $con = mysqli_connect(HOST,USER,PASS,DB) or die('Unable to Connect');
    
        
        $sql = "INSERT INTO ccs_transactions (trans_id, queue_code, data_key, datetime, stud_num, firstname, middlename, lastname, program, purp, term, sy, status, contact_num) VALUES ('$transID', '$fireID', '$id123', '$unix', '$id', '$firstname', '$middlename', '$lastname', '$lcourse', '$top', '$gcurrent_term', '$gcurrent_sy', '$status', '$contactnum')";
        
        
        
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