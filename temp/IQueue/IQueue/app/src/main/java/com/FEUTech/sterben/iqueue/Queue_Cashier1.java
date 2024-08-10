package com.FEUTech.sterben.iqueue;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioGroup;

import com.nispok.snackbar.Snackbar;
import com.nispok.snackbar.SnackbarManager;
import com.weiwangcn.betterspinner.library.material.MaterialBetterSpinner;

import java.util.HashMap;

import me.drakeet.materialdialog.MaterialDialog;

import static com.FEUTech.sterben.iqueue.R.id;
import static com.FEUTech.sterben.iqueue.R.layout;

public class Queue_Cashier1 extends AppCompatActivity {

    public static final String ROOT_URL = "http://iqueuesystem.com";
    HttpParse httpParse = new HttpParse();
    String HttpURL = "http://iqueuesystem.com/steb/checkCashierifEmpty.php";
    HashMap<String, String> hashMap = new HashMap<>();
    private Button nextbtn, add_another;
    private ProgressDialog progressDialog;
    private EditText others;
    private Context context;
    private RadioGroup rg, rg2;
    private MaterialDialog mMaterialDialog2;
    private MaterialBetterSpinner spinner;
    //private RadioButton rb1, rb2, rb3, rb4, rb5, rb6, rb7;
    private String typeOfPayment, yearterm, spinyear, schoolID, ID, finalResult, holder = "";
    private String[] SPINNERLIST = {"20152016", "20162017", "20172018", "20182019"};

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(layout.queue_cashier1);

        Intent in = getIntent();
        Bundle b = in.getExtras();
        schoolID = b.getString("num");
//        holder = b.getString("anotherTransaction");
        ID = schoolID;
//        if(TextUtils.isEmpty(holder)) {
//            holder = "";
//            Toast.makeText(Queue_Cashier1.this, holder, Toast.LENGTH_LONG).show();
//        } else {
//            Toast.makeText(Queue_Cashier1.this, holder, Toast.LENGTH_LONG).show();
//        }

        context = this;
        ArrayAdapter<String> arrayAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_dropdown_item_1line, SPINNERLIST);
        spinner = (MaterialBetterSpinner) findViewById(id.spinner2);
        spinner.setAdapter(arrayAdapter);

        others = (EditText) findViewById(id.other);
        nextbtn = (Button) findViewById(id.nextbtn);
        rg = (RadioGroup) findViewById(id.radioGroup);
        rg2 = (RadioGroup) findViewById(id.radioGroup2);
        add_another = (Button) findViewById(id.add_another);

        add_another.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(Queue_Cashier1.this, Queue_Cashier1_add.class));
            }
        });

        /*rb1 = (RadioButton) findViewById(id.rb1);
        rb2 = (RadioButton) findViewById(id.rb2);
        rb3 = (RadioButton) findViewById(id.rb3);
        rb4 = (RadioButton) findViewById(id.rb4);
        rb5 = (RadioButton) findViewById(id.rb5);
        rb6 = (RadioButton) findViewById(id.rb6);
        rb7 = (RadioButton) findViewById(id.rb7);*/
//        existingqueuechecker();

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        nextbtn();
        hasInternetConnection();
        radioListener();
        UserLoginFunction(ID);
    }

    public void UserLoginFunction(final String email) {

        class UserLoginClass5 extends AsyncTask<String, Void, String> {

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
//
                progressDialog = new ProgressDialog(Queue_Cashier1.this, R.style.MyTheme);
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
//                        Queue_Cashier1.this);

                if (!httpResponseMsg.equals("0")) {

                    mMaterialDialog2 = new MaterialDialog(Queue_Cashier1.this)
                            .setTitle("Sorry iTam")
                            .setMessage("Only one queue per transaction is allowed")
                            .setPositiveButton("Done", new View.OnClickListener() {
                                @Override
                                public void onClick(View v) {
                                    Bundle bi = new Bundle();
                                    bi.putString("num", ID);
                                    Intent i = new Intent(Queue_Cashier1.this, Screen_Add_Queue.class);
                                    i.putExtras(bi);
                                    startActivity(i);
                                    overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
                                    mMaterialDialog2.dismiss();
                                    finish();
                                }
                            });
                    mMaterialDialog2.show();

                } else {
//
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

        UserLoginClass5 userLoginClass2 = new UserLoginClass5();
        userLoginClass2.execute(email);
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
        rg.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup radioGroup, int i) {
                int id = rg.getCheckedRadioButtonId();
                switch (id) {
                    case R.id.rb1:
//                        add_another.setVisibility(View.VISIBLE);
                        others.setVisibility(View.GONE);
//                        mMaterialDialog2 = new MaterialDialog(Queue_Cashier1.this)
//                                .setTitle("Sample")
////                                .setMessage("Only one queue per transaction is allowed")
//                                .setPositiveButton("Done", new View.OnClickListener() {
//                                    @Override
//                                    public void onClick(View v) {
//                                    }
//                                });
//                        mMaterialDialog2.show();
                        break;
                    case R.id.rb2:
//                        add_another.setVisibility(View.VISIBLE);
                        others.setVisibility(View.GONE);
                        break;
                    case R.id.rb3:
                        others.setVisibility(View.VISIBLE);
//                        add_another.setVisibility(View.VISIBLE);
                        break;
                    default:
                        others.setVisibility(View.GONE);
                        add_another.setVisibility(View.GONE);
                        break;
                }
            }
        });
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Bundle bi = new Bundle();
        bi.putString("num", ID);
        Intent i = new Intent(Queue_Cashier1.this, Screen_Add_Queue.class);
        i.putExtras(bi);
        startActivity(i);

        //overridePendingTransition(0,0);
        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_in);
        finish();
        return true;
    }

    @Override
    public void onBackPressed() {
        Bundle bi = new Bundle();
        bi.putString("num", ID);
        Intent i = new Intent(Queue_Cashier1.this, Screen_Add_Queue.class);
        i.putExtras(bi);
        startActivity(i);
//        overridePendingTransition(0,0);
        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
        finish();
    }

    private void nextbtn() {

        if (hasInternetConnection()) {
            nextbtn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    int hep = 0;
                    switch (rg2.getCheckedRadioButtonId()) {
                        case -1:
                            SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"), Queue_Cashier1.this);
                            break;
                        case id.rb4:
                            yearterm = "1st Term";
                            hep = hep + 1;
                            break;
                        case id.rb5:
                            yearterm = "2nd Term";
                            hep = hep + 1;
                            break;
                        case id.rb6:
                            yearterm = "3rd Term";
                            hep = hep + 1;
                            break;
                        case id.rb7:
                            yearterm = "4th Term";
                            hep = hep + 1;
                            break;
                        default:
                            SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"), Queue_Cashier1.this);
                            break;
                    }
                    switch (rg.getCheckedRadioButtonId()) {
                        case -1:
                            SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"), Queue_Cashier1.this);
                            break;
                        case id.rb1:
                            typeOfPayment = "Down Payment";
                            hep = hep + 1;
                            break;
                        case id.rb2:
                            typeOfPayment = "Balance";
                            hep = hep + 1;
                            break;
                        case id.rb3:
                            if (others.getText().toString().isEmpty() || others.getText().toString().equals("")) {
                                others.setError("Required");
                                SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"), Queue_Cashier1.this);
                            } else {
                                typeOfPayment = others.getText().toString();
                                hep = hep + 1;
                            }
                            break;
                        default:
                            SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"), Queue_Cashier1.this);
                            break;
                    }
                    if (spinner.getText().toString().isEmpty()) {
                        SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"), Queue_Cashier1.this);
                    } else {
                        spinyear = spinner.getText().toString().trim();
                        hep = hep + 1;
                    }

                    if (hep == 3) {

                        Intent passData = new Intent(Queue_Cashier1.this, Queue_Cashier2.class);
                        Bundle bundle = new Bundle();
                        bundle.putString("ID", ID);
                        bundle.putString("data_term", yearterm);
                        bundle.putString("data_type", typeOfPayment + " " + holder);
                        bundle.putString("data_year", spinyear);
                        passData.putExtras(bundle);
                        startActivity(passData);
                        overridePendingTransition(0, 0);
                        finish();
                    } else {
                        SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"), Queue_Cashier1.this);
                    }
                }
            });
        } else {
            SnackbarManager.show(Snackbar.with(getApplicationContext()).text("No internet connection"), Queue_Cashier1.this);
        }
    }

}


