package com.FEUTech.sterben.iqueue;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.support.v7.app.AppCompatActivity;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.google.firebase.FirebaseApp;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.nispok.snackbar.Snackbar;
import com.nispok.snackbar.SnackbarManager;

import org.jetbrains.annotations.NonNls;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Random;

import cn.pedant.SweetAlert.SweetAlertDialog;
import me.drakeet.materialdialog.MaterialDialog;
import retrofit.Callback;
import retrofit.RestAdapter;
import retrofit.RetrofitError;
import retrofit.client.Response;

public class Queue_Accounting extends AppCompatActivity {

    @NonNls
    public static final String ROOT_URL = "http://iqueuesystem.com";
    HttpParse httpParse = new HttpParse();
    String HttpURL = "http://iqueuesystem.com/steb/checkAccountingIfEmpty.php";
    HashMap<String, String> hashMap = new HashMap<>();
    DatabaseReference DatabaseFirebaseCashier;
    private Context context;
    private String ID, typeOfPayment, contactnum, finalResult, myqueue, lname, fname, mname, full_name, course1, id123, fireId, gcurrent_term, gcurrent_schoolyear;
    private RadioGroup acc_radgroup;
    private RadioButton rb1, rb2, rb3;
    private EditText acc_others;
    private Button acc_addqueue;
    private MaterialDialog mMaterialDialog, mMaterialDialog2;
    private ProgressDialog progressDialog;
    private Handler handler = new Handler();
    private Runnable runnable;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_queue__accounting);

        FirebaseApp.initializeApp(this);
        Intent in = getIntent();
        Bundle b = in.getExtras();
        ID = b.getString("num");
        DatabaseFirebaseCashier = FirebaseDatabase.getInstance().getReference("Accounting Transactions");

        acc_addqueue = (Button) findViewById(R.id.acc_addqueue);
        acc_radgroup = (RadioGroup) findViewById(R.id.acc_radioGroup);
        acc_others = (EditText) findViewById(R.id.other_accounting);

        context = this;

        if (!hasInternetConnection()) {
            Bundle bi = new Bundle();
            bi.putString("num", ID);
            Intent i = new Intent(Queue_Accounting.this, Screen_Add_Queue.class);
            i.putExtras(bi);
            startActivity(i);
            overridePendingTransition(R.anim.push_right_in, R.anim.push_right_in);
            finish();
        }
        id123 = DatabaseFirebaseCashier.push().getKey();
        GenerateRandomString();
        getSqlDetails();
        nextbtn();
        radioListener();
        UserLoginFunction(ID);
    }

    private void GenerateRandomString() {

        @NonNls final String DATA = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        Random RANDOM = new Random();
        int len = 20;
        StringBuilder sb = new StringBuilder(len);
        for (int i = 0; i < len; i++) {
            sb.append(DATA.charAt(RANDOM.nextInt(DATA.length())));
        }
        fireId = sb.toString().toLowerCase();
    }

    private boolean hasInternetConnection() {
        ConnectivityManager cm = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo wifiNetwork = cm.getNetworkInfo(ConnectivityManager.TYPE_WIFI);
        if (wifiNetwork != null && wifiNetwork.isConnected()) {
            return true;
        }
        NetworkInfo mobileNetwork = cm.getNetworkInfo(ConnectivityManager.TYPE_MOBILE);
        if (mobileNetwork != null && mobileNetwork.isConnected()) {
            return true;
        }
        NetworkInfo activeNetwork = cm.getActiveNetworkInfo();
        return activeNetwork != null && activeNetwork.isConnected();
    }

    private void radioListener() {
        acc_radgroup.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup radioGroup, int i) {
                int id = acc_radgroup.getCheckedRadioButtonId();
                switch (id) {
                    case R.id.acc_rb3:
                        acc_others.setVisibility(View.VISIBLE);
                        break;
                    default:
                        acc_others.setVisibility(View.GONE);
                        break;
                }
            }
        });
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Bundle bi = new Bundle();
        bi.putString("num", ID);
        Intent i = new Intent(Queue_Accounting.this, Screen_Add_Queue.class);
        i.putExtras(bi);
        startActivity(i);
        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_in);
        finish();
        return true;
    }

    @Override
    public void onBackPressed() {
        Bundle bi = new Bundle();
        bi.putString("num", ID);
        Intent i = new Intent(Queue_Accounting.this, Screen_Add_Queue.class);
        i.putExtras(bi);
        startActivity(i);
        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
        finish();
    }

    private void getMyQueue() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/getmycurrentqueue_accounting.php?studentID=" + ID;
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
                                String myqueue1;

                                myqueue1 = jsonobject.getString("queue_num");

                                myqueue = myqueue1;
                            }
                        } catch (JSONException e) {
                            e.printStackTrace();

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
                                            .text("Network Problem"), Queue_Accounting.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void getSqlDetails() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/getStudentdata.php?studentID=" + ID;
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
                                String course = jsonobject.getString("program").trim();
                                String firstname = jsonobject.getString("firstname").trim();
                                String middlename = jsonobject.getString("middlename").trim();
                                String lastname = jsonobject.getString("lastname").trim();
                                String current_schoolyear = jsonobject.getString("current_sy").trim();
                                String current_term = jsonobject.getString("current_term").trim();
                                contactnum = jsonobject.getString("contact_num").trim();
                                course1 = course;
                                lname = lastname;
                                fname = firstname;
                                mname = middlename;
                                gcurrent_schoolyear = current_schoolyear;
                                gcurrent_term = current_term;

                                full_name = fname + " " + mname + " " + lname;
                            }
                        } catch (JSONException e) {
                            e.printStackTrace();
                            SnackbarManager.show(
                                    Snackbar.with(getApplicationContext())
                                            .text("Something went wrong on fetching data"), Queue_Accounting.this);

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
                                            .text("Network Problem"), Queue_Accounting.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }

        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void insertUser() {
        progressDialog = new ProgressDialog(Queue_Accounting.this, R.style.MyTheme);
        progressDialog.setCancelable(false);
        progressDialog.setProgressStyle(android.R.style.Widget_ProgressBar_Small);
        progressDialog.show();

        RestAdapter adapter = new RestAdapter.Builder().setEndpoint(ROOT_URL).build();
        Queue_Accounting_Send_Data api = adapter.create(Queue_Accounting_Send_Data.class);
        api.insertUserAccounting(
                ID, fireId, id123, gcurrent_term, typeOfPayment, gcurrent_schoolyear, course1, fname, mname, lname, contactnum,
                new Callback<Response>() {
                    @Override
                    public void success(final Response result, Response response) {
                        getMyQueue();
                        new Handler().postDelayed(new Runnable() {
                            @Override
                            public void run() {

                                String zero = "0";
                                String TransId = "0";
                                Queue_FirebaseAdapter queue_firebaseAdapter = new Queue_FirebaseAdapter(fireId,
                                        myqueue, full_name, ID, typeOfPayment, zero, TransId, "0");

                                DatabaseFirebaseCashier.child(id123).setValue(queue_firebaseAdapter);
                                Bundle bi = new Bundle();
                                bi.putString("num", ID);
                                Intent i = new Intent(Queue_Accounting.this, Pane_Accounting.class);
                                i.putExtras(bi);
                                progressDialog.dismiss();
                                materialcaller();
//                                        SnackbarManager.show(Snackbar.with(getApplicationContext()).
//                                                text(myqueue), Queue_Accounting.this);
                            }
                        }, 2500);
                    }

                    @Override
                    public void failure(RetrofitError error) {
                        Toast.makeText(Queue_Accounting.this, "Please try again", Toast.LENGTH_LONG).show();
                        progressDialog.dismiss();
                        sweetAlerterror();
                    }
                }
        );
    }


    private void sweetAlerterror() {
        new SweetAlertDialog(this, SweetAlertDialog.ERROR_TYPE)
                .setTitleText("Oops...")
                .setContentText("Something went wrong. Please try again")
                .show();
    }

    private void nextbtn() {
        acc_addqueue.setOnClickListener(new OnClickListener() {
            @Override
            public void onClick(View v) {
                switch (acc_radgroup.getCheckedRadioButtonId()) {
                    case -1:
                        SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"), Queue_Accounting.this);
                        break;
                    case R.id.acc_rb1:
                        typeOfPayment = "Promissory Note";
                        insertUser();
                        break;
                    case R.id.acc_rb2:
                        typeOfPayment = "Uniform";
                        insertUser();
                        break;
                    case R.id.acc_rb3:
                        if (acc_others.getText().toString().isEmpty() || acc_others.getText().toString().equals("")) {
                            acc_others.setError("Required");
                            SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"), Queue_Accounting.this);
                        } else {
                            typeOfPayment = acc_others.getText().toString();
                        }
                        insertUser();
                        break;
                    default:
                        //SnackbarManager.show(Snackbar.with(getApplicationContext()).text(typeOfPayment), Queue_Accounting.this);

                        break;
                }
            }
        });

    }

    public void UserLoginFunction(final String email) {

        class UserLoginClass3 extends AsyncTask<String, Void, String> {

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
//
                progressDialog = new ProgressDialog(Queue_Accounting.this, R.style.MyTheme);
                progressDialog.setCancelable(false);
                progressDialog.setProgressStyle(android.R.style.Widget_ProgressBar_Small);
                progressDialog.show();
            }

            @Override
            protected void onPostExecute(String httpResponseMsg) {

                super.onPostExecute(httpResponseMsg);
//
                progressDialog.dismiss();
//                SnackbarManager.show(Snackbar.with(getApplicationContext()).text(httpResponseMsg),
//                        Queue_Accounting.this);

                if (!httpResponseMsg.equals("0")) {
//
                    mMaterialDialog2 = new MaterialDialog(Queue_Accounting.this)
                            .setTitle("Sorry iTam")
                            .setMessage("Only one queue per transaction is allowed")
                            .setPositiveButton("Done", new View.OnClickListener() {
                                @Override
                                public void onClick(View v) {
                                    Bundle bi = new Bundle();
                                    bi.putString("num", ID);
                                    Intent i = new Intent(Queue_Accounting.this, Screen_Add_Queue.class);
                                    i.putExtras(bi);
                                    startActivity(i);
                                    overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
                                    mMaterialDialog2.dismiss();
                                    finish();

                                }
                            });
                    mMaterialDialog2.show();

                } else {


//                    UserLoginFunction(ID);
                }
            }

            @Override
            protected String doInBackground(String... params) {


                hashMap.put("studnum", params[0]);

                finalResult = httpParse.postRequest(hashMap, HttpURL);

                return finalResult;

            }

        }

        UserLoginClass3 userLoginClass2 = new UserLoginClass3();
        userLoginClass2.execute(email);
    }

    private void materialcaller() {

        mMaterialDialog = new MaterialDialog(this)
                .setTitle("Queue successful")
                .setMessage("Transaction added for queue")
                .setPositiveButton("OK", new OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        mMaterialDialog.dismiss();
                        Bundle bi = new Bundle();
                        bi.putString("num", ID);
                        Intent i = new Intent(Queue_Accounting.this, Pane_Accounting.class);
                        i.putExtras(bi);
                        startActivity(i);
                        overridePendingTransition(R.anim.push_left_in, R.anim.push_left_out);
                        finish();
                    }
                });

        mMaterialDialog.show();
    }
}
