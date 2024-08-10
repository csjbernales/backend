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
$sql = "select * from cashier_transaction";

$result = mysqli_query($connection, $sql) or die("Error in Selecting " . mysqli_error($connection));

//create an array
$emparray = array();
while ($row = mysqli_fetch_assoc($result)) {
    $emparray[] = $row;
}
echo json_encode($emparray);

//close the db connection
mysqli_close($connection);

?>