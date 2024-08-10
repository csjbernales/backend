package com.FEUTech.sterben.iqueue;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.NotificationCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.view.MenuItem;
import android.widget.TextView;

import com.android.volley.Request;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.ValueEventListener;
import com.nispok.snackbar.Snackbar;
import com.nispok.snackbar.SnackbarManager;

import org.jetbrains.annotations.NonNls;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class Queue_Empty extends AppCompatActivity {

    TextView textView, textView6, textView8, textView9, textView10, textView11, textView89, textView12;
    private String s2, hold, cashier_number, type, id, office, lname, fname, mname, full_name, pop;
    private Handler handler = new Handler();
    private Runnable runnable;


    private void getSqlDetails() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/getStudentdata.php?studentID=" + s2;
        //String url = "http://192.168.43.56/data.php?phone=" + number;
        StringRequest stringRequest = new StringRequest(Request.Method.GET,
                url,
                new com.android.volley.Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        try {
                            JSONArray jsonarray = new JSONArray(response);
                            for (int i = 0; i < jsonarray.length(); i++) {
                                JSONObject jsonobject = jsonarray.getJSONObject(i);
                                String firstname = jsonobject.getString("firstname").trim();
                                String middlename = jsonobject.getString("middlename").trim();
                                String lastname = jsonobject.getString("lastname").trim();
                                lname = lastname;
                                fname = firstname;
                                mname = middlename;

                                full_name = fname + " " + mname + " " + lname;
                                if (!hold.equals("0")) {
                                    textView10.setText(full_name);
                                }

                            }
                        } catch (JSONException e) {
                            e.printStackTrace();
                            SnackbarManager.show(
                                    Snackbar.with(getApplicationContext())
                                            .text("Something went wrong on fetching data"), Queue_Empty.this);

                            handler.removeCallbacks(runnable);
                        }
                    }
                },
                new com.android.volley.Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        if (error != null) {

                            SnackbarManager.show(
                                    Snackbar.with(getApplicationContext())
                                            .text("Network Problem"), Queue_Empty.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }

        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.queue_empty);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        textView = (TextView) findViewById(R.id.textView88);
        textView6 = (TextView) findViewById(R.id.textView6);
        textView8 = (TextView) findViewById(R.id.textView8);
        textView9 = (TextView) findViewById(R.id.textView9);
        textView10 = (TextView) findViewById(R.id.textView10);
        textView11 = (TextView) findViewById(R.id.textView11);
        textView89 = (TextView) findViewById(R.id.textView89);
        textView12 = (TextView) findViewById(R.id.textView12);

        //getWindow().setFlags(WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS, WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS);

        Intent in = getIntent();
        Bundle b = in.getExtras();
        assert b != null;
        s2 = b.getString("num");
        hold = b.getString("reference");
        type = b.getString("office_type");
        office = b.getString("office");
        pop = b.getString("pop");

        getSqlDetails();

        if (hold.equals("0")) {
            textView.setText(R.string.youhave_emptyqueue);

//            SnackbarManager.show(
//                    Snackbar.with(getApplicationContext())
//                            .text("You have no queued transaction"), Queue_Empty.this);
        } else if (hold.equals("1")) {
            switch (type) {
                case "CCS":

                    FirebaseDatabase firebaseDatabase = FirebaseDatabase.getInstance();

                    final DatabaseReference databaseReferenceMyCurrentQueue = firebaseDatabase
                            .getReference("CCS Transactions");

                    databaseReferenceMyCurrentQueue.orderByChild("myCurrentQueue").limitToFirst(1)
                            .addValueEventListener(new ValueEventListener() {
                                @Override
                                public void onDataChange(DataSnapshot dataSnapshot) {
                                    //String queuenumber = String.valueOf(dataSnapshot.getValue());
                                    //currentqueue1.setText(queuenumber);
                                    for (DataSnapshot ds : dataSnapshot.getChildren()) {
                                        id = ds.child("myCurrentQueue").getValue(String.class);
                                        cashier_number = ds.child("counter").getValue(String.class);

                                        String call = ds.child("call").getValue(String.class);
                                        if (call.equals("2")) {
                                            notificationAlert();
                                        }

                                        textView.setText("Please proceed at the ");
                                        textView6.setText(type + " office");
                                        textView8.setText("Counter number");
                                        textView11.setText(cashier_number);
                                        textView89.setText("Your queue number:");
                                        textView9.setText(id);
                                        textView12.setText(pop);

                                        if (id.equals("0")) {
                                            textView.setText("Queue Empty");
                                            textView6.setText("");
                                            textView8.setText("");
                                            textView11.setText("");
                                            textView89.setText("");
                                            textView9.setText("");
                                            textView12.setText("");
                                        }

//                                        SnackbarManager.show(
//                                                Snackbar.with(getApplicationContext())
//                                                        .text("Please proceed at the cashier number " + cashier_number), Queue_Empty.this);
//                                    Log.d("tag", id);
                                        //String queuenumber = String.valueOf(dataSnapshot.getValue());
                                    }
                                }

                                @Override
                                public void onCancelled(DatabaseError databaseError) {

                                }
                            });
                    break;
                case "accounting":

                    FirebaseDatabase firebaseDatabase4 = FirebaseDatabase.getInstance();

                    final DatabaseReference databaseReferenceMyCurrentQueue4 = firebaseDatabase4
                            .getReference("Accounting Transactions");

                    databaseReferenceMyCurrentQueue4.orderByChild("myCurrentQueue").limitToFirst(1)
                            .addValueEventListener(new ValueEventListener() {
                                @Override
                                public void onDataChange(DataSnapshot dataSnapshot) {
                                    //String queuenumber = String.valueOf(dataSnapshot.getValue());
                                    //currentqueue1.setText(queuenumber);
                                    for (DataSnapshot ds : dataSnapshot.getChildren()) {
                                        id = ds.child("myCurrentQueue").getValue(String.class);
                                        cashier_number = ds.child("counter").getValue(String.class);
                                        String call = ds.child("call").getValue(String.class);
                                        if (call.equals("2")) {
                                            notificationAlert();
                                        }

                                        textView.setText("Please proceed at the ");
                                        textView6.setText(type + " office");
                                        textView8.setText("Counter number");
                                        textView11.setText(cashier_number);
                                        textView89.setText("Your queue number:");
                                        textView9.setText(id);
                                        textView12.setText(pop);
                                        if (id.equals("0")) {
                                            textView.setText("Queue Empty");
                                            textView6.setText("");
                                            textView8.setText("");
                                            textView11.setText("");
                                            textView89.setText("");
                                            textView9.setText("");
                                            textView12.setText("");
                                        }

//                                        SnackbarManager.show(
//                                                Snackbar.with(getApplicationContext())
//                                                        .text("Please proceed at the cashier number " + cashier_number), Queue_Empty.this);
//                                    Log.d("tag", id);
                                        //String queuenumber = String.valueOf(dataSnapshot.getValue());
                                    }
                                }

                                @Override
                                public void onCancelled(DatabaseError databaseError) {

                                }
                            });
                    break;
                case "cashier":
                    FirebaseDatabase firebaseDatabase2 = FirebaseDatabase.getInstance();

                    final DatabaseReference databaseReferenceMyCurrentQueue2 = firebaseDatabase2
                            .getReference("Cashier Transactions");

                    databaseReferenceMyCurrentQueue2.orderByChild("myCurrentQueue").limitToFirst(1)
                            .addValueEventListener(new ValueEventListener() {
                                @Override
                                public void onDataChange(DataSnapshot dataSnapshot) {
                                    //String queuenumber = String.valueOf(dataSnapshot.getValue());
                                    //currentqueue1.setText(queuenumber);
                                    for (DataSnapshot ds : dataSnapshot.getChildren()) {
                                        id = ds.child("myCurrentQueue").getValue(String.class);
                                        cashier_number = ds.child("counter").getValue(String.class);

                                        String call = ds.child("call").getValue(String.class);
                                        if (call.equals("2")) {
                                            notificationAlert();
                                        }

                                        textView.setText("Please proceed at the ");
                                        textView6.setText(type + " office");
                                        textView8.setText("Counter number");
                                        textView11.setText(cashier_number);
                                        textView89.setText("Your queue number:");
                                        textView9.setText(id);
                                        textView12.setText(pop);
                                        if (id.equals("0")) {
                                            textView.setText("Queue Empty");
                                            textView6.setText("");
                                            textView8.setText("");
                                            textView11.setText("");
                                            textView89.setText("");
                                            textView9.setText("");
                                            textView12.setText("");
                                        }

//                                    Log.d("tag", id);
                                        //String queuenumber = String.valueOf(dataSnapshot.getValue());
                                    }
                                }

                                @Override
                                public void onCancelled(DatabaseError databaseError) {

                                }
                            });
                    break;
                case "registrar":

                    FirebaseDatabase firebaseDatabase3 = FirebaseDatabase.getInstance();

                    final DatabaseReference databaseReferenceMyCurrentQueue3 = firebaseDatabase3
                            .getReference("Registrar Transactions");

                    databaseReferenceMyCurrentQueue3.orderByChild("myCurrentQueue").limitToFirst(1)
                            .addValueEventListener(new ValueEventListener() {
                                @Override
                                public void onDataChange(DataSnapshot dataSnapshot) {
                                    //String queuenumber = String.valueOf(dataSnapshot.getValue());
                                    //currentqueue1.setText(queuenumber);
                                    for (DataSnapshot ds : dataSnapshot.getChildren()) {
                                        id = ds.child("myCurrentQueue").getValue(String.class);
                                        cashier_number = ds.child("counter").getValue(String.class);

                                        String call = ds.child("call").getValue(String.class);
                                        if (call.equals("2")) {
                                            notificationAlert();
                                        }

                                        textView.setText("Please proceed at the ");
                                        textView6.setText(type + " office");
                                        textView8.setText("Counter number");
                                        textView11.setText(cashier_number);
                                        textView89.setText("Your queue number:");
                                        textView9.setText(id);
                                        textView12.setText(pop);
                                        if (id.equals("0")) {
                                            textView.setText("Queue Empty");
                                            textView6.setText("");
                                            textView8.setText("");
                                            textView11.setText("");
                                            textView89.setText("");
                                            textView9.setText("");
                                            textView12.setText("");
                                        }

//                                        SnackbarManager.show(
//                                                Snackbar.with(getApplicationContext())
//                                                        .text("Please proceed at the cashier number " + cashier_number), Queue_Empty.this);
//                                    Log.d("tag", id);
                                        //String queuenumber = String.valueOf(dataSnapshot.getValue());
                                    }
                                }

                                @Override
                                public void onCancelled(DatabaseError databaseError) {

                                }
                            });
                    break;
            }
            notifFunction();
        }
    }

    private void notifFunction() {

        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                notificationAlert();
            }
        }, 700);

    }

    public void notificationAlert() {
        Intent intent = new Intent(this, Queue_Empty.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
        PendingIntent pendingIntent = PendingIntent.getActivity(this, 1410,
                intent, PendingIntent.FLAG_ONE_SHOT);
        NotificationCompat.Builder notificationBuilder = new
                NotificationCompat.Builder(this)
                .setSmallIcon(R.drawable.feutech_iqueue)
                .setContentTitle("Please proceed at the " + type + " counter")
                .setContentText("Counter number " + cashier_number + " .Your queue number is " + id + " for " + pop)
                .setAutoCancel(true)
                .setVibrate(new long[]{1000, 1000, 1000})
                .setColor(ContextCompat.getColor(getApplicationContext(), R.color.colorPrimary))
                .setDefaults(Notification.DEFAULT_ALL)
                .setPriority(Notification.PRIORITY_MAX)
                .setContentIntent(pendingIntent);

        NotificationManager notificationManager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);

        assert notificationManager != null;
        notificationManager.notify(1410, notificationBuilder.build());
    }

    @Override
    public void onBackPressed() {
        Intent in = getIntent();
        Bundle b = in.getExtras();
        assert b != null;
        s2 = b.getString("num");
        Bundle bi = new Bundle();
        bi.putString("num", s2);
        Intent i = new Intent(Queue_Empty.this, Screen_My_Queue.class);
        i.putExtras(bi);
        startActivity(i);
        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
        finish();
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        //handler.removeCallbacks(runnable);
        Intent in = getIntent();
        Bundle b = in.getExtras();
        assert b != null;
        s2 = b.getString("num");
        Bundle bi = new Bundle();
        bi.putString("num", s2);
        Intent i = new Intent(Queue_Empty.this, Screen_Home.class);
        i.putExtras(bi);
        startActivity(i);
        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);

        finish();
        return true;
    }
}
