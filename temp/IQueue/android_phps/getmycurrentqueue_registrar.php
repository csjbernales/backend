<?php

//Define your host here.
$HostName = "localhost";

//Define your database username here.
$HostUser = "foobar2018";

//Define your database password here.
$HostPass = "foobar2018";

//Define your database name here.
$DatabaseName = "thesis_db";

$connection = mysqli_connect($HostName, $HostUser, $HostPass, $DatabaseName) or die("Error " . mysqli_error($connection));

$studentID = $_GET['studentID'];

$query = "SELECT * FROM reg_transactions where stud_num ='{$studentID}'";

$result = mysqli_query($connection, $query);

while (($row = mysqli_fetch_assoc($result)) == true) {
    
    $data[] = $row;
    
}

echo json_encode($data);

?>
