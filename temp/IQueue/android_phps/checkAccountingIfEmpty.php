<?php

 if($_SERVER['REQUEST_METHOD']=='POST'){

//Define your host here.
$HostName = "localhost";

//Define your database username here.
$HostUser = "foobar2018";

//Define your database password here.
$HostPass = "foobar2018";

//Define your database name here.
$DatabaseName = "thesis_db";
 
 $con = mysqli_connect($HostName,$HostUser,$HostPass,$DatabaseName);
 
 $studnum = $_POST['studnum'];
//  $studnum = "201510140";
 
 $Sql_Query = "select count(1) from ao_transactions where stud_num = $studnum";
 
 $check = mysqli_fetch_array(mysqli_query($con, $Sql_Query));
 
 if(isset($check)) {
    
    echo $check[0]."";
 
 }
 
 else {
 
 echo "Invalid Username. Please Try Again";
 
 }
 
 } else {
 
 echo "Check Again";
 
 }
 
 mysqli_close($con);

?>