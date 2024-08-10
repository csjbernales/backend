
<?php
$mysql_hostname = "localhost";
$mysql_user = "foobar2018";
$mysql_password = "foobar2018";//CHANGE THIS
$mysql_database = "thesis_db";//CHANGE THIS


$con = mysqli_connect($mysql_hostname, $mysql_user, $mysql_password, $mysql_database);

$qr = $_POST["qr"];

$sql = "SELECT * FROM Dept_sched where prof_availability = 1";
$shout = mysqli_query($con,$sql);

$array['items'] = array();
while($row = mysqli_fetch_array($shout)){
	array_push($array['items'], array('prof_name'=>$row[1]));
}

echo json_encode($array);

?>