<?php

//checking if the script received a post request or not 
if ($_SERVER['REQUEST_METHOD'] == 'POST') {

    //Getting post data 
    $contactnum  = $_POST['id'];
    $id = $_POST['contactnum'];
    $datacode = $_POST['datacode'];
    $firebaseKey = $_POST['fireID'];
    $term = $_POST['term'];
    $top = $_POST['top'];
    $year = $_POST['year'];
    $course = $_POST['course'];
    $firstname = $_POST['firstname'];
    $middlename = $_POST['middlename'];
    $lastname = $_POST['lastname'];
    $type = $_POST['type'];
    $checknum = $_POST['checknum'];
    $branch = $_POST['branch'];
    $total = $_POST['totalamount'];


    $status = "Pending";
    $unix = time();

/*
    //checking if the received values are blank
    if ($name == '' || $username == '' || $password == '' || $email == '') {
        //giving a message to fill all values if the values are blank
        echo 'please fill all values';
    } else {*/
        //If the values are not blank
        //Connecting to our database by calling dbConnect script 
    define('HOST','localhost');
    define('USER','foobar2018');
    define('PASS','foobar2018');
    define('DB','thesis_db');
    
    $con = mysqli_connect(HOST,USER,PASS,DB) or die('Unable to Connect');
    

        //If username is not already exist 
        //Creating insert query 
        $sql = "INSERT INTO cashier_transaction (contact_num, datetime, queue_code, data_key, stud_num, program, pop, term, sy, aop, total, firstname, middlename, lastname, status, branch, checknum) 
        VALUES('$contactnum', '$unix', '$datacode', '$firebaseKey', '$id','$course','$top','$term','$year','$type','$total','$firstname','$middlename','$lastname','$status', '$branch', '$checknum')";

        //Trying to insert the values to db 
        if (mysqli_query($con, $sql)) {
            //If inserted successfully 
            echo 'Queue added.';
        } else {
            //In case any error occured 
            echo 'Please try again.';
        }

        //Closing the database connection 
        mysqli_close($con);
    
    /*}*/
} else {
    echo 'error';
}
?>