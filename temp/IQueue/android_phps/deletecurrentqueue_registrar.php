<!DOCTYPE html>

<?php
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $id = $_POST['number'];
    define('HOST','localhost');
    define('USER','foobar2018');
    define('PASS','foobar2018');
    define('DB','thesis_db');
    
    $con = mysqli_connect(HOST,USER,PASS,DB) or die('Unable to Connect');
    
        $sql = " DELETE FROM reg_transactions WHERE stud_num = '$id' ";
        if (mysqli_query($con, $sql)) {
            echo 'Queue deleted';
        } else {
            echo 'Please try again.';
        }
        mysqli_close($con);
} else {
    echo 'error';
}
