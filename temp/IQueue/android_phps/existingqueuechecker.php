<?php

//Define your host here.
$HostName = "localhost";

//Define your database username here.
$HostUser = "foobar2018";

//Define your database password here.
$HostPass = "foobar2018";

//Define your database name here.
$DatabaseName = "thesis_db";

//open connection to mysql db
$connection = mysqli_connect($HostName, $HostUser, $HostPass, $DatabaseName) or die("Error " . mysqli_error($connection));

//fetch table rows from mysql db
$sql = "select count(*) as count from cashier_transaction";
$string = array("key" => "John");
//create an array
echo json_encode($string);

?>


