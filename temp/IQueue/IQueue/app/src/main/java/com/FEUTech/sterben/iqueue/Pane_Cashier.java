package com.FEUTech.sterben.iqueue;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.NotificationCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.Query;
import com.google.firebase.database.ValueEventListener;
import com.nispok.snackbar.Snackbar;
import com.nispok.snackbar.SnackbarManager;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import me.drakeet.materialdialog.MaterialDialog;
import retrofit.Callback;
import retrofit.RestAdapter;
import retrofit.RetrofitError;

public class Pane_Cashier extends AppCompatActivity {

    public static final String ROOT_URL = "http://iqueuesystem.com";
    private TextView currentqueue1, specifiedqueue1, pop1;
    private String number, call, transactiontype, queustatus;
    private String myqueueGlobalHolder, gCashier_number;
    private MaterialDialog mMaterialDialog, mMaterialDialog2;
    private Boolean consistentChecker;
    private Handler handler2;
    private DatabaseReference databaseReference;
    private Runnable runnable2;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.pane_cashier);


        databaseReference = FirebaseDatabase.getInstance().getReference("Cashier Transactions");
//        final ProgressDialog progressDialog = ProgressDialog.show(Pane_Cashier.this,"Please Wait",
//                "Processing...",true);
//        progressDialog.show();
        // ReQueue();
        Intent in = getIntent();
        Bundle b = in.getExtras();
        number = b.getString("num");
        currentqueue1 = (TextView) findViewById(R.id.currentqueue);
        specifiedqueue1 = (TextView) findViewById(R.id.specifiedqueue);
        pop1 = (TextView) findViewById(R.id.pop);

        getMyQueue();

//
//        runnable = new Runnable() {
//            @Override
//            public void run() {
//                //function
//                getSqlDetails();
//                handler.postDelayed(this, 1000);
//            }
//        };
//        handler.postDelayed(runnable, 1000);

        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                FirebaseDatabase firebaseDatabase = FirebaseDatabase.getInstance();

                final DatabaseReference databaseReferenceMyCurrentQueue = firebaseDatabase
                        .getReference("Cashier Transactions");

                databaseReferenceMyCurrentQueue.orderByChild("myCurrentQueue").limitToFirst(1)
                        .addValueEventListener(new ValueEventListener() {
                            @Override
                            public void onDataChange(DataSnapshot dataSnapshot) {
                                //String queuenumber = String.valueOf(dataSnapshot.getValue());
                                //currentqueue1.setText(queuenumber);
                                for (DataSnapshot ds : dataSnapshot.getChildren()) {
                                    String id = ds.child("myCurrentQueue").getValue(String.class);
                                    String getCall = ds.child("call").getValue(String.class);
                                    String qs = ds.child("queueStatus").getValue(String.class);
                                    queustatus = qs;
                                    int qn_holder = Integer.parseInt(id) + 1;
                                    String id_plus_one = String.valueOf(qn_holder);
                                    //   Log.d("tag", id);
                                    call = getCall;
                                    if (qs.equals("0")) {
                                        currentqueue1.setText(id);
                                    } else {
                                        currentqueue1.setText(id_plus_one);
                                    }
                                    //String queuenumber = String.valueOf(dataSnapshot.getValue());
                                }
                            }

                            @Override
                            public void onCancelled(DatabaseError databaseError) {

                            }
                        });


//                if(queustatus.equals("On Hold") || queustatus.equals("1")) {
//                    mMaterialDialog = new MaterialDialog(Pane_Cashier.this)
//                            .setTitle("Your queue have been moved")
//                            .setMessage("please proceed at the specified counter first")
//                            .setPositiveButton("OK", new View.OnClickListener() {
//                                @Override
//                                public void onClick(View v) {
//                                    finish();
//                                }
//                            });
//                }
            }
        }, 500);

        queueChecker();
        final ProgressDialog progress = new ProgressDialog(this);
        progress.setTitle("Connecting");
        progress.setMessage("Please wait...");
        progress.setCancelable(false);
        progress.show();

        Runnable progressRunnable = new Runnable() {

            @Override
            public void run() {
                progress.cancel();
            }
        };

        Handler pdCanceller = new Handler();
        pdCanceller.postDelayed(progressRunnable, 4000);
    }

    // private void ReQueue() {
    //     findViewById(R.id.requeue).setOnClickListener(new View.OnClickListener() {
    //         @Override
    //         public void onClick(View v) {

    //             mMaterialDialog = new MaterialDialog(Pane_Cashier.this)
    //                     .setTitle("ReQueue")
    //                     .setMessage("You will be prioritized if you are skipped")
    //                     .setNegativeButton("Cancel", new View.OnClickListener() {
    //                         @Override
    //                         public void onClick(View v) {
    //                             //cancel
    //                             mMaterialDialog.dismiss();
    //                         }
    //                     })
    //                     .setPositiveButton("OK", new View.OnClickListener() {
    //                         @Override
    //                         public void onClick(View v) {
    //                             DatabaseReference ref = FirebaseDatabase.getInstance().getReference();
    //                             Query query = ref.child("Cashier Transactions").orderByChild("myCurrentQueue").equalTo(myqueueGlobalHolder);

    //                             query.addListenerForSingleValueEvent(new ValueEventListener() {
    //                                 @Override
    //                                 public void onDataChange(DataSnapshot dataSnapshot) {
    //                                     for (DataSnapshot dataSnapshot1 : dataSnapshot.getChildren()) {
    //                                         String zero = "0";
    //                                         String counter = "0";
    //                                         Queue_FirebaseAdapter_Requeue queue_firebaseAdapter = new Queue_FirebaseAdapter(id123, "0");

    //                                         databaseReference.child(id123).setValue(queue_firebaseAdapter);
    //                                     }
    //                                     mMaterialDialog.dismiss();
    //                                 }

    //                                 @Override
    //                                 public void onCancelled(DatabaseError databaseError) {
    //                                     mMaterialDialog.dismiss();
    //                                     SnackbarManager.show(
    //                                             Snackbar.with(getApplicationContext())
    //                                                     .text("Network Problem"), Pane_Cashier.this);
    //                                 }
    //                             });
    //                         }
    //                     });
    //         }
    //     });
    // }

    private void queueChecker() {
        handler2 = new Handler();
        consistentChecker = true;
//        final Intent intent = new Intent(Pane_Cashier.this, Queue_Cashier_Service.class);
//        intent.putExtra("specifiedQueue", String.valueOf(specifiedqueue1));
//        startService(intent);
        runnable2 = new Runnable() {
            @Override
            public void run() {
                if (specifiedqueue1.getText().toString().trim().isEmpty() || (specifiedqueue1.getText().toString().trim().equals(""))
                        || (specifiedqueue1.getText().toString().trim().equals("00"))) {
                    consistentChecker = false;
                    cancelflags();
                    Bundle bi = new Bundle();
                    bi.putString("num", number);
                    bi.putString("reference", "0");
//                    Toast.makeText(Pane_Cashier.this, "insidestop", Toast.LENGTH_SHORT).show();
                    Intent i = new Intent(Pane_Cashier.this, Queue_Empty.class);
                    i.putExtras(bi);
//                    stopService(intent);
                    startActivity(i);
                    overridePendingTransition(0, 0);
                    finish();
                } else if (specifiedqueue1.getText().toString().trim().equals(currentqueue1.getText().toString().trim())) {
                    if (call.equals("0")) {
                        consistentChecker = true;
                        handler2.postDelayed(runnable2, 1000);
                    } else {
//                        notificationAlert();
                        consistentChecker = false;
//                    Toast.makeText(Pane_Cashier.this, "insidestop", Toast.LENGTH_SHORT).show();
                        cancelflags();
                        Bundle bi = new Bundle();
                        bi.putString("num", number);
                        bi.putString("reference", "1");
                        bi.putString("office_type", "cashier");
                        bi.putString("pop", transactiontype);
                        Intent i = new Intent(Pane_Cashier.this, Queue_Empty.class);
                        i.putExtras(bi);
                        //mMaterialDialog.dismiss();
                        //stopService(intent);
                        startActivity(i);
                        overridePendingTransition(0, 0);
                        finish();
                    }
                    //materialdialogcaller();
                } else if (consistentChecker) {
//                    Toast.makeText(Pane_Cashier.this, "i", Toast.LENGTH_SHORT).show();
                    handler2.postDelayed(runnable2, 1000);
                }

            }

        };

        handler2.postDelayed(runnable2, 4000);


    }

    public void materialdialogdeleteconfirmation(View v) {

        mMaterialDialog = new MaterialDialog(Pane_Cashier.this)
                .setTitle("Delete queue")
                .setMessage("Confirm cancel of queue?")
                .setNegativeButton("Cancel", new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        //cancel
                        mMaterialDialog.dismiss();
                    }
                })
                .setPositiveButton("OK", new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        DatabaseReference ref = FirebaseDatabase.getInstance().getReference();
                        Query query = ref.child("Cashier Transactions").orderByChild("myCurrentQueue").equalTo(myqueueGlobalHolder);

                        query.addListenerForSingleValueEvent(new ValueEventListener() {
                            @Override
                            public void onDataChange(DataSnapshot dataSnapshot) {
                                for (DataSnapshot dataSnapshot1 : dataSnapshot.getChildren()) {
                                    dataSnapshot1.getRef().removeValue();
                                }
                                mMaterialDialog.dismiss();
                            }

                            @Override
                            public void onCancelled(DatabaseError databaseError) {
                                mMaterialDialog.dismiss();
                                SnackbarManager.show(
                                        Snackbar.with(getApplicationContext())
                                                .text("Network Problem"), Pane_Cashier.this);
                            }
                        });

                        RestAdapter adapter = new RestAdapter.Builder().setEndpoint(ROOT_URL).build();
                        Queue_Cashier_Delete_Data api = adapter.create(Queue_Cashier_Delete_Data.class);
                        api.deleteUser(
                                number, new Callback<retrofit.client.Response>() {
                                    @Override
                                    public void success(final retrofit.client.Response result, retrofit.client.Response response) {

                                        mMaterialDialog.dismiss();

                                        mMaterialDialog2 = new MaterialDialog(Pane_Cashier.this)
                                                .setTitle("Queue Deleted Successfully")
                                                .setMessage("")
                                                .setPositiveButton("Done", new View.OnClickListener() {
                                                    @Override
                                                    public void onClick(View v) {
                                                        //cancel
                                                        Bundle bi = new Bundle();
                                                        bi.putString("num", number);
                                                        Intent i = new Intent(Pane_Cashier.this, Screen_Home.class);
                                                        i.putExtras(bi);
                                                        cancelflags();
                                                        startActivity(i);
                                                        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
                                                        finish();
                                                        mMaterialDialog2.dismiss();
                                                    }
                                                });
                                        mMaterialDialog2.setCanceledOnTouchOutside(false);
                                        mMaterialDialog2.show();
                                    }

                                    @Override
                                    public void failure(RetrofitError error) {
                                        mMaterialDialog.dismiss();
                                        SnackbarManager.show(
                                                Snackbar.with(getApplicationContext())
                                                        .text("Network Problem"), Pane_Cashier.this);
                                    }
                                }
                        );
                    }
                });

        mMaterialDialog.setCanceledOnTouchOutside(false);
        mMaterialDialog.show();

    }

    private void cancelflags() {
        handler2.removeCallbacks(runnable2);
    }

    private void notificationAlert() {
        Intent intent = new Intent(this, Queue_Empty.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
        PendingIntent pendingIntent = PendingIntent.getActivity(this, 1410,
                intent, PendingIntent.FLAG_ONE_SHOT);
        NotificationCompat.Builder notificationBuilder = new
                NotificationCompat.Builder(this)
                .setSmallIcon(R.drawable.feutech_iqueue)
                .setContentTitle("IQueue")
                .setContentText("Please proceed at the counter")
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
    public boolean onOptionsItemSelected(MenuItem item) {
        Bundle bi = new Bundle();
        bi.putString("num", number);
        Intent i = new Intent(Pane_Cashier.this, Screen_Add_Queue.class);
        i.putExtras(bi);
        startActivity(i);
        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
        return true;
    }

    @Override
    public void onBackPressed() {

        Bundle bi = new Bundle();
        bi.putString("num", number);
        Intent i = new Intent(Pane_Cashier.this, Screen_My_Queue.class);
        i.putExtras(bi);
        startActivity(i);
        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);

    }

    private void getMyQueue() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        String url = "http://iqueuesystem.com/steb/getmycurrentqueue.php?studentID=" + number;
        //String url = "http://192.168.43.56/data.php?phone=" + number;
        StringRequest stringRequest = new StringRequest(Request.Method.GET,
                url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {

                        try {

                            JSONArray jsonarray = new JSONArray(response);

                            for (int i = 0; i < jsonarray.length(); i++) {

                                JSONObject jsonobject = jsonarray.getJSONObject(i);
                                String myqueue1, transactfor1;

                                myqueue1 = jsonobject.getString("queue_num");
                                transactfor1 = jsonobject.getString("pop");

                                myqueueGlobalHolder = myqueue1;
                                specifiedqueue1.setText(myqueue1);
                                pop1.setText(transactfor1);
                                transactiontype = transactfor1;

                            }
                        } catch (JSONException e) {
                            e.printStackTrace();
//
//                            Bundle bi = new Bundle();
//                            bi.putString("num", number);
//                            Intent i = new Intent(Pane_Cashier.this, Queue_Empty.class);
//                            i.putExtras(bi);
//                            startActivity(i);
//                            overridePendingTransition(0, 0);
//                            finish();

                        }
                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        if (error != null) {

                            SnackbarManager.show(
                                    Snackbar.with(getApplicationContext())
                                            .text("Network Problem"), Pane_Cashier.this);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

//     private void getMyQueue() {
//         //String url = "http://192.168.254.100/data.php?phone=" + number;
//         String url = "http://iqueuesystem.com/steb/getmycurrentqueue.php?studentID=" + number;
//         //String url = "http://192.168.43.56/data.php?phone=" + number;
//         StringRequest stringRequest = new StringRequest(Request.Method.GET,
//                 url,
//                 new Response.Listener<String>() {
//                     @Override
//                     public void onResponse(String response) {

//                         try {

//                             JSONArray jsonarray = new JSONArray(response);

//                             for (int i = 0; i < jsonarray.length(); i++) {

//                                 JSONObject jsonobject = jsonarray.getJSONObject(i);
//                                 id123 = jsonobject.getString("data_key");

//                             }
//                         } catch (JSONException e) {
//                             e.printStackTrace();
// //
// //                            Bundle bi = new Bundle();
// //                            bi.putString("num", number);
// //                            Intent i = new Intent(Pane_Cashier.this, Queue_Empty.class);
// //                            i.putExtras(bi);
// //                            startActivity(i);
// //                            overridePendingTransition(0, 0);
// //                            finish();

//                         }
//                     }
//                 },
//                 new Response.ErrorListener() {
//                     @Override
//                     public void onErrorResponse(VolleyError error) {
//                         if (error != null) {

//                             SnackbarManager.show(
//                                     Snackbar.with(getApplicationContext())
//                                             .text("Network Problem"), Pane_Cashier.this);
//                         }
//                     }
//                 }
//         );

//         MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
//     }

}