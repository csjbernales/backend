package com.FEUTech.sterben.iqueue;

import android.app.ProgressDialog;
import android.content.ContentValues;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.support.v7.app.AppCompatActivity;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;

import com.android.volley.Request;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.nispok.snackbar.Snackbar;
import com.nispok.snackbar.SnackbarManager;
import com.weiwangcn.betterspinner.library.material.MaterialBetterSpinner;

import org.jetbrains.annotations.NonNls;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLEncoder;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.Random;

import cn.pedant.SweetAlert.SweetAlertDialog;
import me.drakeet.materialdialog.MaterialDialog;
import retrofit.Callback;
import retrofit.RestAdapter;
import retrofit.RetrofitError;
import retrofit.client.Response;

public class Queue_CCS extends AppCompatActivity {

    public static final String ROOT_URL = "http://iqueuesystem.com";
    HttpParse httpParse2 = new HttpParse();
    String HttpURL2 = "http://iqueuesystem.com/steb/checkCCSIfEmpty.php";
    String URLS2 = "http://iqueuesystem.com/steb/prof_availability.php";
    HashMap<String, String> hashMap2 = new HashMap<>();
    ArrayAdapter<String> adapter;
    private MaterialBetterSpinner Spinner1;
    private EditText edtcourseToAdd, edtcourseToDrop, edtDropSection, edtAddSection;
    private Button queueToCCS;
    private String[] spinnertypeoftransaction = {"COR Revision", "Parallel Course Declaration", "Grade Completion",
            "Returnee | Re-admission", "Transferee", "Crediting of Subject", "Canvas", "Faculty Consultation"};
    private String id, course1, fname, lname, mname, full_name, gcurrent_sy, gcurrent_term, top, contactnum;
    private String fireId, id123, myqueue, finalResult2, transID;
    private Context context;
    private MaterialDialog mMaterialDialog, mMaterialDialog2;
    private DatabaseReference DatabaseFirebaseCashier;
    private Handler handler = new Handler();
    private Runnable runnable;
    private ProgressDialog progressDialog;
    private MaterialBetterSpinner profspinner;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.queue_ccs);
        context = this;
        DatabaseFirebaseCashier = FirebaseDatabase.getInstance().getReference("CCS Transactions");

        profspinner = findViewById(R.id.profspinner);
        Spinner1 = (MaterialBetterSpinner) findViewById(R.id.spinner_choose_Transaction_CCS);
        edtcourseToAdd = (EditText) findViewById(R.id.course_add);
        edtcourseToDrop = (EditText) findViewById(R.id.course_drop);
        edtDropSection = (EditText) findViewById(R.id.section_drop);
        edtAddSection = (EditText) findViewById(R.id.section_add);
        queueToCCS = (Button) findViewById(R.id.add_queue_to_CCS);

        ArrayAdapter<String> arspintype = new ArrayAdapter<String>(this,
                android.R.layout.simple_dropdown_item_1line, spinnertypeoftransaction);
        Spinner1.setAdapter(arspintype);

        Intent in = getIntent();
        Bundle b = in.getExtras();
        id = b.getString("num");
//        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        id123 = DatabaseFirebaseCashier.push().getKey();

        GenerateRandomString();
        insertUserToRegistrar();
        hasInternetConnection();
        viewhidden();
        getSqlDetails();
        UserLoginFunction(id);

        new MyTask().execute();

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
        fireId = sb.toString();
    }

    private void viewhidden() {
        Spinner1.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
            }

            @Override
            public void afterTextChanged(Editable s) {
                //String quantity = spinner.getText().toString();
                viewhiddencaller();
            }
        });
    }

    private void viewhiddencaller() {

        @NonNls String quantity = Spinner1.getText().toString();
        if (quantity.equals("COR Revision")) {
            edtcourseToDrop.setVisibility(View.VISIBLE);
            edtAddSection.setVisibility(View.VISIBLE);
            edtcourseToAdd.setVisibility(View.VISIBLE);
            edtDropSection.setVisibility(View.VISIBLE);
            profspinner.setVisibility(View.GONE);
        } else if (quantity.equals("Faculty Consultation")) {
            edtcourseToDrop.setVisibility(View.GONE);
            edtAddSection.setVisibility(View.GONE);
            edtcourseToAdd.setVisibility(View.GONE);
            edtDropSection.setVisibility(View.GONE);
            profspinner.setVisibility(View.VISIBLE);
        } else if (quantity.equals("Faculty Consultation")) {
            edtcourseToDrop.setVisibility(View.GONE);
            edtAddSection.setVisibility(View.GONE);
            edtcourseToAdd.setVisibility(View.GONE);
            edtDropSection.setVisibility(View.GONE);
            profspinner.setVisibility(View.GONE);
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
                        Intent i = new Intent(Queue_CCS.this, Pane_CCS.class);
                        i.putExtras(bi);
                        startActivity(i);
                        overridePendingTransition(R.anim.push_left_in, R.anim.push_left_out);
                        finish();
                    }
                });

        mMaterialDialog.show();
    }

    private void getSqlDetails() {
        @NonNls String url = "http://iqueuesystem.com/steb/getStudentdata.php?studentID=" + id;
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
                                String current_sy = jsonobject.getString("current_sy").trim();
                                String current_term = jsonobject.getString("current_term").trim();
                                contactnum = jsonobject.getString("contact_num").trim();
                                course1 = course;
                                lname = lastname;
                                fname = firstname;
                                mname = middlename;
                                gcurrent_sy = current_sy;
                                gcurrent_term = current_term;


                                full_name = fname + " " + mname + " " + lname;
                            }
                        } catch (JSONException e) {
                            e.printStackTrace();
                            SnackbarManager.show(
                                    Snackbar.with(getApplicationContext())
                                            .text("Something went wrong on fetching data"), Queue_CCS.this);

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
                                            .text("Network Problem"), Queue_CCS.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }

        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void getMyQueue() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/getmycurrentqueue_ccs.php?studentID=" + id;
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
                                            .text("Network Problem"), Queue_CCS.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    public void UserLoginFunction(final String email) {

        class UserLoginClass3 extends AsyncTask<String, Void, String> {

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
//
                progressDialog = new ProgressDialog(Queue_CCS.this, R.style.MyTheme);
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
//                        Queue_Registrar.this);

                if (!httpResponseMsg.equals("0")) {

                    mMaterialDialog2 = new MaterialDialog(Queue_CCS.this)
                            .setTitle("Sorry iTam")
                            .setMessage("Only one queue per transaction is allowed")
                            .setPositiveButton("Done", new View.OnClickListener() {
                                @Override
                                public void onClick(View v) {
                                    Bundle bi = new Bundle();
                                    bi.putString("num", id);
                                    Intent i = new Intent(Queue_CCS.this, Screen_Add_Queue.class);
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


                hashMap2.put("studnum", params[0]);

                finalResult2 = httpParse2.postRequest(hashMap2, HttpURL2);

                return finalResult2;

            }

        }

        UserLoginClass3 userLoginClass2 = new UserLoginClass3();
        userLoginClass2.execute(email);
    }

    private void insertUserToRegistrar() {
        queueToCCS.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if (hasInternetConnection()) {
                    int hold = 0;
                    if (Spinner1.getText().toString().isEmpty()) {
                        SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"),
                                Queue_CCS.this);
                    } else if (Spinner1.getText().toString().equals("COR Revision")) {
                        if (edtAddSection.getText().toString().isEmpty() || edtDropSection.getText().toString().isEmpty()
                                || edtcourseToAdd.getText().toString().isEmpty() || edtcourseToDrop.getText().toString().isEmpty()) {
                            SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"),
                                    Queue_CCS.this);
                        } else {
                            transID = "T01";
                            top = Spinner1.getText().toString() + " of course " + edtcourseToDrop.getText().toString() + " from section " +
                                    edtDropSection.getText().toString() + " to new course " + edtcourseToAdd.getText().toString() +
                                    " on section " + edtAddSection.getText().toString();

                            hold = hold + 1;
                        }
                    } else {
                        if (Spinner1.getText().toString().equals("Parallel Course Declaration")) {
                            transID = "T02";
                        } else if (Spinner1.getText().toString().equals("Grade Completion")) {
                            transID = "T03";
                        } else if (Spinner1.getText().toString().equals("Returnee | Re-admission")) {
                            transID = "T04";
                        } else if (Spinner1.getText().toString().equals("Transferee")) {
                            transID = "T05";
                        } else if (Spinner1.getText().toString().equals("Crediting of Subject")) {
                            transID = "T06";
                        } else if (Spinner1.getText().toString().equals("Canvas")) {
                            transID = "T07";
                        } else if (Spinner1.getText().toString().equals("Faculty Consultation")) {
                            transID = "T08";
                        }

                        top = Spinner1.getText().toString();
                        hold = hold + 1;
                    }
                    if (hold != 0) {
                        progressDialog = new ProgressDialog(Queue_CCS.this, R.style.MyTheme);
                        progressDialog.setCancelable(false);
                        progressDialog.setProgressStyle(android.R.style.Widget_ProgressBar_Small);
                        progressDialog.show();

                        RestAdapter adapter = new RestAdapter.Builder().setEndpoint(ROOT_URL).build();
                        Queue_CCS_Send_Data api = adapter.create(Queue_CCS_Send_Data.class);
                        api.insertUserCCS(
                                id, fireId, id123, course1, fname, mname, lname, top, gcurrent_sy, gcurrent_term, transID, contactnum,

                                new Callback<Response>() {
                                    @Override
                                    public void success(final Response result, Response response) {
                                        getMyQueue();
                                        new Handler().postDelayed(new Runnable() {
                                            @Override
                                            public void run() {
                                                String zero = "0";
                                                String cashier = "0";
                                                Queue_FirebaseAdapterCCS queue_firebaseAdapter = new Queue_FirebaseAdapterCCS(fireId,
                                                        myqueue, full_name, id, top, zero, cashier, transID, "0");

                                                DatabaseFirebaseCashier.child(id123).setValue(queue_firebaseAdapter);
                                                Bundle bi = new Bundle();
                                                bi.putString("num", id);
                                                Intent i = new Intent(Queue_CCS.this, Screen_My_Queue.class);
                                                i.putExtras(bi);
                                                progressDialog.dismiss();
                                                materialcaller();
                                            }
                                        }, 4000);
                                    }

                                    @Override
                                    public void failure(RetrofitError error) {
//                                Toast.makeText(Queue_Registrar.this, "Please try again", Toast.LENGTH_LONG).show();
                                        progressDialog.dismiss();
                                        sweetAlerterror();
                                    }

                                });
                    }

                } else {
                    SnackbarManager.show(Snackbar.with(getApplicationContext()).text("No internet connection"),
                            Queue_CCS.this);
                }
            }
        });
    }


    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Bundle b = new Bundle();
        String IDHolder = id;
        b.putString("num", IDHolder);
        Intent i = new Intent(Queue_CCS.this, Screen_Add_Queue.class);
        i.putExtras(b);
        startActivity(i);
        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
        finish();
        return true;
    }

    @Override
    public void onBackPressed() {
        Bundle b = new Bundle();
        String IDHolder = id;
        b.putString("num", IDHolder);
        Intent i = new Intent(Queue_CCS.this, Screen_Add_Queue.class);
        i.putExtras(b);
        startActivity(i);
        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
        finish();
    }

    public String postValue(ContentValues cv) throws UnsupportedEncodingException {
        StringBuilder sb = new StringBuilder();
        boolean flag = true;

        for (Map.Entry<String, Object> value : cv.valueSet()) {
            if (flag) {
                flag = false;
            } else {
                sb.append("&");
            }
            sb.append(URLEncoder.encode(value.getKey(), "UTF-8"));
            sb.append("=");
            sb.append(URLEncoder.encode(value.getValue().toString(), "UTF-8"));
        }
        return sb.toString();
    }

    class MyTask extends AsyncTask<Void, Void, String> {
        ProgressDialog progressDialog;

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            progressDialog = new ProgressDialog(Queue_CCS.this);
            progressDialog.setMessage("LOADING...");
            progressDialog.setIndeterminate(false);
            progressDialog.setCancelable(true);
            progressDialog.show();
        }

        @Override
        protected String doInBackground(Void... params) {

            try {
                URL url = new URL(URLS2);
                HttpURLConnection con = (HttpURLConnection) url.openConnection();
                con.setRequestMethod("POST");
                con.setDoInput(true);
                con.setDoOutput(true);
                OutputStream os = con.getOutputStream();
                BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(os));
                ContentValues cv = new ContentValues();
                cv.put("qr", "qr");
                String postValue = postValue(cv);
                bw.write(postValue);
                bw.flush();
                bw.close();

                InputStream inputStream = con.getInputStream();
                BufferedReader br = new BufferedReader(new InputStreamReader(inputStream));
                StringBuilder sb = new StringBuilder();
                String str = "";
                while ((str = br.readLine()) != null) {
                    sb.append(str);
                }
                return sb.toString();
            } catch (MalformedURLException e) {
                // Log.i("key","No connection");
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
            return null;
        }

        @Override
        protected void onPostExecute(String strJSON) {
            progressDialog.dismiss();
            parseJSON(strJSON);
        }

        public void parseJSON(String strJSON) {
            try {

                JSONObject jsonObject = new JSONObject(strJSON);
                JSONArray jsonArray = jsonObject.getJSONArray("items");
                int i = 0;
                ArrayList prof = new ArrayList();
                while (jsonArray.length() > i) {
                    jsonObject = jsonArray.getJSONObject(i);

                    String prof_name = jsonObject.getString("prof_name");
                    //Log.i("mylog", prof_name);
                    prof.add(prof_name);

                    i++;

                }

                adapter = new ArrayAdapter<String>(Queue_CCS.this, android.R.layout.simple_dropdown_item_1line, prof);
                profspinner.setAdapter(adapter);


            } catch (Exception e) {
                e.printStackTrace();
            }
        }

    }


}
