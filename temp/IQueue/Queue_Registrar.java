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
import android.text.Editable;
import android.text.TextWatcher;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
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

import java.util.HashMap;
import java.util.Random;

import cn.pedant.SweetAlert.SweetAlertDialog;
import me.drakeet.materialdialog.MaterialDialog;
import retrofit.Callback;
import retrofit.RestAdapter;
import retrofit.RetrofitError;
import retrofit.client.Response;

public class Queue_Registrar extends AppCompatActivity  {

    @NonNls
    public static final String ROOT_URL = "http://iqueuesystem.com";
    HttpParse httpParse2 = new HttpParse();
    String HttpURL2 = "http://iqueuesystem.com/steb/checkRegistrarIfEmpty.php";
    HashMap<String, String> hashMap2 = new HashMap<>();
    private DatabaseReference DatabaseFirebaseCashier;
    private MaterialDialog mMaterialDialog, mMaterialDialog2;
    @NonNls
    private String id, term, year, top, myqueue, fireId, id123, contactnum;
    @NonNls
    private String[] spinnertypeoftransaction = {"COR Printing", "Scholarship Application", "Application for: ", "Claim of: "};
    private String[] spineryear = {"20162017", "20172018", "20182019"};
    private String[] spinnerterm = {"1st Term", "2nd  Term", "3rd Term", "4th Term"};
    private String[] appclaim = {"Certificates", "Diploma", "Honorable Dismissal", "Transcript of Records"};
    private MaterialBetterSpinner spinner1, spinner2, spinner3, spinner4, spinner5;
    private ProgressDialog progressDialog;
    private String course1, lname, fname, mname, gcurrent_sy, gcurrent_term, cert_availability, checkIfAvailable,
            app_availability, appdiploma_availability, apphd_availability, apptor_availability, content_of_checkandapp,
            claimtor_availability, claimhd_availability, claimdiploma_availability;
    private Context context;
    private String full_name, finalResult1, finalResult2, finalResult3;
    private Handler handler = new Handler();
    private Runnable runnable;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.queue_registrar);
        FirebaseApp.initializeApp(this);
        DatabaseFirebaseCashier = FirebaseDatabase.getInstance().getReference("Registrar Transactions");
        context = this;
        ArrayAdapter<String> arspintype = new ArrayAdapter<String>(this, android.R.layout.simple_dropdown_item_1line, spinnertypeoftransaction);
        ArrayAdapter<String> arspinyear = new ArrayAdapter<String>(this, android.R.layout.simple_dropdown_item_1line, spineryear);
        ArrayAdapter<String> arspinterm = new ArrayAdapter<String>(this, android.R.layout.simple_dropdown_item_1line, spinnerterm);
        ArrayAdapter<String> appfor = new ArrayAdapter<String>(this, android.R.layout.simple_dropdown_item_1line, appclaim);
        ArrayAdapter<String> claimfor = new ArrayAdapter<String>(this, android.R.layout.simple_dropdown_item_1line, appclaim);
        spinner1 = (MaterialBetterSpinner) findViewById(R.id.spinner_registrartype);
        spinner2 = (MaterialBetterSpinner) findViewById(R.id.spinner_registrarprintingschoolyear);
        spinner3 = (MaterialBetterSpinner) findViewById(R.id.spinner_registrarprintingterm);
        spinner4 = (MaterialBetterSpinner) findViewById(R.id.spinner_registrarappfor);
        spinner5 = (MaterialBetterSpinner) findViewById(R.id.spinner_registrarclaimfor);
        spinner1.setAdapter(arspintype);
        spinner2.setAdapter(arspinyear);
        spinner3.setAdapter(arspinterm);
        spinner4.setAdapter(appfor);
        spinner5.setAdapter(claimfor);
        Intent in = getIntent();
        Bundle b = in.getExtras();
        id = b.getString("num");
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        Button submit = (Button) findViewById(R.id.add_queuetoresgistrar);
        id123 = DatabaseFirebaseCashier.push().getKey();
        GenerateRandomString();
        getSqlDetails();
        viewhidden();
        viewhidden2();
        viewhidden3();
        hasInternetConnection();
        UserLoginFunction2(id);

        getAppIfAvailable();
        getdiplomaIfAvailable();
        gethdIfAvailable();
        gettorIfAvailable();

        getClaimIfAvailable();
        getClaimIfAvailable_diploma();
        getClaimIfAvailable_hd();
        getClaimIfAvailable_tor();


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
        spinner1.addTextChangedListener(new TextWatcher() {
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

    private void viewhidden2() {
        spinner4.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
            }

            @Override
            public void afterTextChanged(Editable s) {
                //String quantity = spinner.getText().toString();
                viewhiddencaller2();
            }
        });
    }

    private void viewhidden3() {
        spinner5.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
            }

            @Override
            public void afterTextChanged(Editable s) {
                //String quantity = spinner.getText().toString();
                viewhiddencaller3();
            }
        });
    }

    private void checkspin4() {
        if(checkIfAvailable.equals("0")) {
//            Toast.makeText(this, "false", Toast.LENGTH_SHORT).show();
            mMaterialDialog2 = new MaterialDialog(Queue_Registrar.this)
                    .setTitle("Sorry iTam")
                    .setMessage("Your queuing for " + content_of_checkandapp + " is still not available right now. Please try again" +
                            " after some time.")
                    .setPositiveButton("OK", new View.OnClickListener() {
                        @Override
                        public void onClick(View v) {
                            Bundle bi = new Bundle();
                            bi.putString("num", id);
                            Intent i = new Intent(Queue_Registrar.this, Screen_Add_Queue.class);
                            i.putExtras(bi);
                            startActivity(i);
                            overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
                            mMaterialDialog2.dismiss();
                            finish();
                        }
                    });
            mMaterialDialog2.show();
        } else {
//            Toast.makeText(this, "true", Toast.LENGTH_SHORT).show();
        }
    }

    private void viewhiddencaller() {

        @NonNls String quantity = spinner1.getText().toString();
        if (quantity.equals("COR Printing")) {
            spinner2.setVisibility(View.VISIBLE);
            spinner3.setVisibility(View.VISIBLE);
            spinner4.setVisibility(View.GONE);
            spinner5.setVisibility(View.GONE);
        } else if (quantity.equals("Scholarship Application")) {
            spinner2.setVisibility(View.GONE);
            spinner3.setVisibility(View.GONE);
            spinner4.setVisibility(View.GONE);
            spinner5.setVisibility(View.GONE);
        } else if (quantity.equals("Application for: ")) {
            spinner2.setVisibility(View.GONE);
            spinner3.setVisibility(View.GONE);
            spinner4.setVisibility(View.VISIBLE);
            spinner5.setVisibility(View.GONE);

        } else if (quantity.equals("Claim of: ")) {
            spinner2.setVisibility(View.GONE);
            spinner3.setVisibility(View.GONE);
            spinner4.setVisibility(View.GONE);
            spinner5.setVisibility(View.VISIBLE);
        }
    }

     private void viewhiddencaller2() {

         @NonNls String quantity2 = spinner4.getText().toString();

//         Toast.makeText(context, quantity2, Toast.LENGTH_SHORT).show();
         if (quantity2.equals("Transcript of Records")) {
             checkIfAvailable = apptor_availability;
             content_of_checkandapp = quantity2;
             checkspin4();
         } else if (quantity2.equals("Honorable Dismissal")) {
             checkIfAvailable = apphd_availability;
             content_of_checkandapp = quantity2;
             checkspin4();
         } else if (quantity2.equals("Diploma")) {
             checkIfAvailable = appdiploma_availability;
             content_of_checkandapp = quantity2;
             checkspin4();
         } else if (quantity2.equals("Certificates")) {
             checkIfAvailable = app_availability;
             content_of_checkandapp = quantity2;
             checkspin4();
         }
     }

    private void viewhiddencaller3() {

        @NonNls String quantity2 = spinner5.getText().toString();

        if (quantity2.equals("Transcript of Records")) {
            checkIfAvailable = claimtor_availability;
            content_of_checkandapp = quantity2;
            checkspin4();
        } else if (quantity2.equals("Honorable Dismissal")) {
            checkIfAvailable = claimhd_availability;
            content_of_checkandapp = quantity2;
            checkspin4();
        } else if (quantity2.equals("Diploma")) {
            checkIfAvailable = claimdiploma_availability;
            content_of_checkandapp = quantity2;
            checkspin4();
        } else if (quantity2.equals("Certificates")) {
            checkIfAvailable = cert_availability;
            content_of_checkandapp = quantity2;
            checkspin4();
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
                        Intent i = new Intent(Queue_Registrar.this, Pane_Registrar.class);
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
                                            .text("Something went wrong on fetching data"), Queue_Registrar.this);

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
                                            .text("Network Problem"), Queue_Registrar.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }

        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void getMyQueue() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/getmycurrentqueue_registrar.php?studentID=" + id;
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
                                            .text("Network Problem"), Queue_Registrar.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void getAppIfAvailable() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/reg_app_cert_checker.php?studentID=" + id;
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

                                myqueue1 = jsonobject.getString("availability");

                                app_availability = myqueue1;
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
                                            .text("Network Problem"), Queue_Registrar.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void getdiplomaIfAvailable() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/reg_app_cert_checker_diploma.php?studentID=" + id;
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

                                myqueue1 = jsonobject.getString("availability");

                                appdiploma_availability = myqueue1;
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
                                            .text("Network Problem"), Queue_Registrar.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void gethdIfAvailable() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/reg_app_cert_checker_hd.php?studentID=" + id;
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

                                myqueue1 = jsonobject.getString("availability");

                                apphd_availability = myqueue1;
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
                                            .text("Network Problem"), Queue_Registrar.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void gettorIfAvailable() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/reg_app_cert_checker_tor.php?studentID=" + id;
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

                                myqueue1 = jsonobject.getString("availability");

                                apptor_availability = myqueue1;
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
                                            .text("Network Problem"), Queue_Registrar.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void getClaimIfAvailable() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/reg_claim_cert_checker.php?studentID=" + id;
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

                                myqueue1 = jsonobject.getString("availability");

                                cert_availability = myqueue1;
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
                                            .text("Network Problem"), Queue_Registrar.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void getClaimIfAvailable_diploma() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/reg_claim_cert_checker_diploma.php?studentID=" + id;
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

                                myqueue1 = jsonobject.getString("availability");

                                claimdiploma_availability = myqueue1;
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
                                            .text("Network Problem"), Queue_Registrar.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void getClaimIfAvailable_hd() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/reg_claim_cert_checker_hd.php?studentID=" + id;
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

                                myqueue1 = jsonobject.getString("availability");

                                claimhd_availability = myqueue1;
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
                                            .text("Network Problem"), Queue_Registrar.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    private void getClaimIfAvailable_tor() {
        //String url = "http://192.168.254.100/data.php?phone=" + number;
        @NonNls String url = "http://iqueuesystem.com/steb/reg_claim_cert_checker_tor.php?studentID=" + id;
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

                                myqueue1 = jsonobject.getString("availability");

                                claimtor_availability = myqueue1;
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
                                            .text("Network Problem"), Queue_Registrar.this);
                            handler.removeCallbacks(runnable);
                        }
                    }
                }
        );

        MySingleton.getInstance(getApplicationContext()).addToRequestQueue(stringRequest);
    }

    public void UserLoginFunction2(final String email) {

        class UserLoginClass3 extends AsyncTask<String, Void, String> {

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
//
                progressDialog = new ProgressDialog(Queue_Registrar.this, R.style.MyTheme);
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

                    mMaterialDialog2 = new MaterialDialog(Queue_Registrar.this)
                            .setTitle("Sorry iTam")
                            .setMessage("Only one queue per transaction is allowed")
                            .setPositiveButton("Done", new View.OnClickListener() {
                                @Override
                                public void onClick(View v) {
                                    Bundle bi = new Bundle();
                                    bi.putString("num", id);
                                    Intent i = new Intent(Queue_Registrar.this, Screen_Add_Queue.class);
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

    public void insertUserToRegistrar(View view) {
        if (hasInternetConnection()) {
            int hold = 0;
            if (spinner1.getText().toString().isEmpty()) {
                SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"), Queue_Registrar.this);
            } else if (spinner1.getText().toString().equals("COR Printing")) {
                top = spinner1.getText().toString().trim();
                String one = spinner1.getText().toString();
                String two = spinner2.getText().toString();
                String three = spinner3.getText().toString();
                if(one.isEmpty() || two.isEmpty() || three.isEmpty()) {
                    SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"), Queue_Registrar.this);
                }else {
                    top = one + " " + two + " " + three + " " ;
                    hold = hold + 1;
                }
            } else if (spinner1.getText().toString().equals("Scholarship Application")) {
                top = spinner1.getText().toString().trim();
                hold = hold + 1;
            } else if (spinner1.getText().toString().equals("Application for: ")) {
                String one = spinner1.getText().toString();
                if (spinner4.getText().toString().isEmpty()) {
                    SnackbarManager.show(Snackbar.with(getApplicationContext()).
                            text("Please fill all fields"), Queue_Registrar.this);
                } else {
                    String two = spinner4.getText().toString();
                    top = one + two;
                    hold = hold + 1;
                }
            } else if (spinner1.getText().toString().equals("Claim of: ")) {
                String one = spinner1.getText().toString();
                if (spinner5.getText().toString().isEmpty()) {
                    SnackbarManager.show(Snackbar.with(getApplicationContext()).
                            text("Please fill all fields"), Queue_Registrar.this);
                } else {
                    String two = spinner5.getText().toString();
                    top = one + two;
                    hold = hold + 1;
                }
            }
            if(hold != 0) {
                progressDialog = new ProgressDialog(Queue_Registrar.this, R.style.MyTheme);
                progressDialog.setCancelable(false);
                progressDialog.setProgressStyle(android.R.style.Widget_ProgressBar_Small);
                progressDialog.show();

                RestAdapter adapter = new RestAdapter.Builder().setEndpoint(ROOT_URL).build();
                Queue_Registrar_Send_Data api = adapter.create(Queue_Registrar_Send_Data.class);
                api.insertUserRegistrar(
                        id, fireId, id123, course1, fname, mname, lname, top, gcurrent_sy, gcurrent_term, contactnum,

                        new Callback<Response>() {
                            @Override
                            public void success(final Response result, Response response) {
                                getMyQueue();
                                new Handler().postDelayed(new Runnable() {
                                    @Override
                                    public void run() {
                                        String zero = "0";
                                        String cashier = "0";
                                        Queue_FirebaseAdapter queue_firebaseAdapter = new Queue_FirebaseAdapter(fireId,
                                                myqueue, full_name, id, top, zero, cashier, "0");

                                        DatabaseFirebaseCashier.child(id123).setValue(queue_firebaseAdapter);
                                        Bundle bi = new Bundle();
                                        bi.putString("num", id);
                                        Intent i = new Intent(Queue_Registrar.this, Screen_My_Queue.class);
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
            SnackbarManager.show(Snackbar.with(getApplicationContext()).text("No internet connection"), Queue_Registrar.this);
        }
    }


    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Bundle b = new Bundle();
        String IDHolder = id;
        b.putString("num", IDHolder);
        Intent i = new Intent(Queue_Registrar.this, Screen_Add_Queue.class);
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
        Intent i = new Intent(Queue_Registrar.this, Screen_Add_Queue.class);
        i.putExtras(b);
        startActivity(i);
        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
        finish();
    }
}
