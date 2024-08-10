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
 
 $studNum = $_POST['studnum'];
 
 $Sql_Query = "select * from client_accounts where stud_num = '$studNum'";
 
 $check = mysqli_fetch_array(mysqli_query($con, $Sql_Query));
 
 if(isset($check)) {
 
 echo "Data Matched";
 }
 else{
 echo "Invalid Username. Please Try Again";
 }
 
 }else{
 echo "Check Again";
 }
mysqli_close($con);

?>