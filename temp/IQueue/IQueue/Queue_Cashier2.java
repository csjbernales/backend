package com.FEUTech.sterben.iqueue;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.os.Handler;
import android.support.design.widget.TextInputLayout;
import android.support.v7.app.AppCompatActivity;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.google.firebase.FirebaseApp;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.nispok.snackbar.Snackbar;
import com.nispok.snackbar.SnackbarManager;
import com.weiwangcn.betterspinner.library.material.MaterialBetterSpinner;

import org.jetbrains.annotations.NonNls;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Random;

import cn.pedant.SweetAlert.SweetAlertDialog;
import me.drakeet.materialdialog.MaterialDialog;
import retrofit.Callback;
import retrofit.RestAdapter;
import retrofit.RetrofitError;
import retrofit.client.Response;

public class Queue_Cashier2 extends AppCompatActivity implements View.OnClickListener {

    @NonNls
    public static final String ROOT_URL = "http://iqueuesystem.com";
    DatabaseReference DatabaseFirebaseCashier;
    MaterialDialog mMaterialDialog;
    @NonNls
    String id, term, year, top, type, branch1, checknum1, myqueue, fireId, id123, contactnum;
    @NonNls
    String[] SPINNERLIST = {"Cash", "BPI", "Check"};
    MaterialBetterSpinner spinner;
    ProgressDialog progressDialog;
    TextInputLayout textInputLayout1, textInputLayout2, textInputLayout3;
    String course1, lname, fname, mname;
    Context context;
    String full_name;
    private EditText branch;
    private EditText checknum;
    private EditText totalamount;
    private Button submit;
    private Handler handler = new Handler();
    private Runnable runnable;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.queue_cashier2);
        FirebaseApp.initializeApp(this);
        DatabaseFirebaseCashier = FirebaseDatabase.getInstance().getReference("Cashier Transactions");
        context = this;
        ArrayAdapter<String> arrayAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_dropdown_item_1line, SPINNERLIST);
        spinner = (MaterialBetterSpinner) findViewById(R.id.spinner2);
        spinner.setAdapter(arrayAdapter);
        Intent in = getIntent();
        @NonNls Bundle b = in.getExtras();
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        textInputLayout1 = (TextInputLayout) findViewById(R.id.textInputLayout8);
        textInputLayout2 = (TextInputLayout) findViewById(R.id.textInputLayout9);
        textInputLayout3 = (TextInputLayout) findViewById(R.id.textInputLayout10);
        textInputLayout3.setVisibility(View.GONE);
        id = b.getString("ID");
        term = b.getString("data_term");
        top = b.getString("data_type");
        year = b.getString("data_year");

        branch = (EditText) findViewById(R.id.branch);
        totalamount = (EditText) findViewById(R.id.amount);
        checknum = (EditText) findViewById(R.id.checknum);

        submit = (Button) findViewById(R.id.add_queue);

        id123 = DatabaseFirebaseCashier.push().getKey();
        GenerateRandomString();

        getSqlDetails();
        submit.setOnClickListener(this);
        viewhidden();
        hasInternetConnection();
    }

    public boolean hasInternetConnection() {
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

    private void viewhidden() {
        spinner.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
            }

            @Override
            public void afterTextChanged(Editable s) {
                String quantity = spinner.getText().toString();
                viewhiddencaller();
            }
        });
    }

    private void viewhiddencaller() {

        @NonNls String quantity = spinner.getText().toString();
        if (quantity.equals("BPI")) {
            textInputLayout1.setVisibility(View.VISIBLE);
            textInputLayout3.setVisibility(View.VISIBLE);
            textInputLayout2.setVisibility(View.VISIBLE);
            branch.setVisibility(View.VISIBLE);
            checknum.setVisibility(View.VISIBLE);
        } else if(quantity.equals("Check")) {
            textInputLayout1.setVisibility(View.VISIBLE);
            textInputLayout3.setVisibility(View.VISIBLE);
            textInputLayout2.setVisibility(View.VISIBLE);
            branch.setVisibility(View.VISIBLE);
            checknum.setVisibility(View.VISIBLE);
        }else {
            branch.setVisibility(View.GONE);
            textInputLayout3.setVisibility(View.VISIBLE);
            checknum.setVisibility(View.GONE);
            textInputLayout1.setVisibility(View.GONE);
            textInputLayout2.setVisibility(View.GONE);
        }

    }

    private void sweetAlerterror() {
        new SweetAlertDialog(this, SweetAlertDialog.ERROR_TYPE)
                .setTitleText("Oops...")
                .setContentText("Something went wrong. Please try again")
                .show();
    }

    private void materialcaller() {

        mMaterialDialog = new MaterialDialog(this)
                .setTitle("Queue successful")
                .setMessage("Transaction added for queue")
                .setPositiveButton("OK", new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        mMaterialDialog.dismiss();
                        Bundle bi = new Bundle();
                        bi.putString("num", id);
                        Intent i = new Intent(Queue_Cashier2.this, Pane_Cashier.class);
                        i.putExtras(bi);
                        startActivity(i);
                        overridePendingTransition(R.anim.push_left_in, R.anim.push_left_out);
                        finish();
                    }
                });

        mMaterialDialog.show();

        /*
        SweetAlertDialog pDialog = new SweetAlertDialog(this, SweetAlertDialog.SUCCESS_TYPE);
        pDialog.getProgressHelper().setBarColor(Color.parseColor("#0c5c00"));
        pDialog.setTitleText("Success!");
        pDialog.setCancelable(false);
        pDialog.show();*/

    }

    private void getSqlDetails() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/getStudentdata.php?studentID=" + id;
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
                                String contactNum = jsonobject.getString("contact_num").trim();
                                contactnum = contactNum;
                                course1 = course;
                                lname = lastname;
                                fname = firstname;
                                mname = middlename;

                                full_name = fname + " " + mname + " " + lname;
                            }
                        } catch (JSONException e) {
                            e.printStackTrace();
                            SnackbarManager.show(
                                    Snackbar.with(getApplicationContext())
                                            .text("Something went wrong on fetching data"), Queue_Cashier2.this);

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
                                            .text("Network Problem"), Queue_Cashier2.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }

        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void getMyQueue() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/getmycurrentqueue.php?studentID=" + id;
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
                                            .text("Network Problem"), Queue_Cashier2.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void insertUser() {
        if (hasInternetConnection()) {
            type = spinner.getText().toString().trim();
            int hep = 0;
            if (type.isEmpty()) {
                SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"),
                        Queue_Cashier2.this);
            } else {
                if (type.equals("BPI")) {
                    if (totalamount.getText().toString().isEmpty()) {
                        totalamount.setError("Required");
                        SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill al fields"),
                                Queue_Cashier2.this);
                    } else if (branch.getText().toString().isEmpty()) {
                        branch.setError("Required");
                        SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill al fields"),
                                Queue_Cashier2.this);
                    } else if (checknum.getText().toString().isEmpty()) {
                    checknum.setError("Required");
                    SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill al fields"),
                            Queue_Cashier2.this);
                    } else {
                        branch1 = branch.getText().toString().trim();
                        checknum1 = checknum.getText().toString().trim();
                        hep = hep + 1;
                    }
                } else if(type.equals("Check")) {
                    if (totalamount.getText().toString().isEmpty()) {
                        totalamount.setError("Required");
                        SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill al fields"),
                                Queue_Cashier2.this);
                    } else if (branch.getText().toString().isEmpty()) {
                        branch.setError("Required");
                        SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill al fields"),
                                Queue_Cashier2.this);
                    } else if (checknum.getText().toString().isEmpty()) {
                    checknum.setError("Required");
                    SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill al fields"),
                            Queue_Cashier2.this);
                    } else {
                        branch1 = branch.getText().toString().trim();
                        checknum1 = checknum.getText().toString().trim();
                        hep = hep + 1;
                    }
                } else if (type.equals("Cash")) {
                    if (totalamount.getText().toString().isEmpty()) {
                        totalamount.setError("Required");
                        SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"),
                                Queue_Cashier2.this);
                    } else {
                        hep = hep + 1;
                    }
                }
            }
            if (hep == 1) {
                progressDialog = new ProgressDialog(Queue_Cashier2.this, R.style.MyTheme);
                progressDialog.setCancelable(false);
                progressDialog.setProgressStyle(android.R.style.Widget_ProgressBar_Small);
                progressDialog.show();

                RestAdapter adapter = new RestAdapter.Builder().setEndpoint(ROOT_URL).build();
                Queue_Cashier_Send_Data api = adapter.create(Queue_Cashier_Send_Data.class);
                api.insertUser(
                        contactnum, id, fireId, id123, term, top, year, course1, fname, mname, lname, totalamount.getText().toString(),
                        branch1, checknum1, totalamount.getText().toString(),
                        new Callback<Response>() {
                            @Override
                            public void success(final Response result, Response response) {
                                getMyQueue();
                                new Handler().postDelayed(new Runnable() {
                                    @Override
                                    public void run() {
                                        String zero = "0";
                                        String counter = "0";
                                        Queue_FirebaseAdapter queue_firebaseAdapter = new Queue_FirebaseAdapter(fireId,
                                                myqueue, full_name, id, top, zero, counter, "0");

                                        DatabaseFirebaseCashier.child(id123).setValue(queue_firebaseAdapter);
                                        Bundle bi = new Bundle();
                                        bi.putString("num", id);
                                        Intent i = new Intent(Queue_Cashier2.this, Pane_Cashier.class);
                                        i.putExtras(bi);
                                        progressDialog.dismiss();
                                        materialcaller();
                                    }
                                }, 4000);
                            }

                            @Override
                            public void failure(RetrofitError error) {
                                Toast.makeText(Queue_Cashier2.this, "Please try again", Toast.LENGTH_LONG).show();
                                progressDialog.dismiss();
                                sweetAlerterror();
                            }
                        }
                );
            }

        } else {
            SnackbarManager.show(Snackbar.with(getApplicationContext()).text("No internet connection"), Queue_Cashier2.this);
        }
    }


    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Bundle b = new Bundle();
        String IDHolder = id;
        b.putString("num", IDHolder);
        Intent i = new Intent(Queue_Cashier2.this, Queue_Cashier1.class);
        i.putExtras(b);
        startActivity(i);
        overridePendingTransition(0,0);
        finish();
        return true;
    }

    @Override
    public void onClick(View v) {
        insertUser();
    }

    @Override
    public void onBackPressed() {
        Bundle b = new Bundle();
        String IDHolder = id;
        b.putString("num", IDHolder);
        Intent i = new Intent(Queue_Cashier2.this, Queue_Cashier1.class);
        i.putExtras(b);
        startActivity(i);
        overridePendingTransition(0, 0);
        finish();
    }
}
