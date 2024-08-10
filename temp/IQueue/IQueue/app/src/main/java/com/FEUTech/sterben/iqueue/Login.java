package com.FEUTech.sterben.iqueue;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.view.KeyEvent;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;

import com.nispok.snackbar.Snackbar;
import com.nispok.snackbar.SnackbarManager;

import java.util.HashMap;

import me.drakeet.materialdialog.MaterialDialog;

public class Login extends AppCompatActivity {

    EditText username;
    String usernameHolder;
    String finalResult;
    HttpParse httpParse = new HttpParse();
    HashMap<String, String> hashMap = new HashMap<>();
    MaterialDialog mMaterialDialog;
    String HttpURL = "http://iqueuesystem.com/steb/UserLogin.php";
    Boolean CheckEditText;
    ProgressDialog progressDialog;
    Context context;
    Button loginButton;

    @Override
    public void onBackPressed() {
        mMaterialDialog = new MaterialDialog(this)
                .setTitle("iQueue")
                .setMessage("Quit application?")
                .setPositiveButton("Exit", new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        finishAffinity();
                        finish();
                    }
                })
                .setNegativeButton("Cancel", new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {//do nothing

                        mMaterialDialog.dismiss();
                    }
                });
        mMaterialDialog.show();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN | WindowManager.LayoutParams.SOFT_INPUT_ADJUST_PAN);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.login);
        loginButton = (Button) findViewById(R.id.login);
        context = this;
        username = (EditText) findViewById(R.id.input_userID);

        username.setOnKeyListener(new View.OnKeyListener() {
            public boolean onKey(View v, int keyCode, KeyEvent event) {
                if (event.getAction() == KeyEvent.ACTION_DOWN) {
                    switch (keyCode) {
                        case KeyEvent.KEYCODE_DPAD_CENTER:
                        case KeyEvent.KEYCODE_ENTER:

                            CheckEditTextIsEmptyOrNot();

                            if (CheckEditText) {

                                UserLoginFunction(usernameHolder);

                            } else if (usernameHolder.length() != 9) {

                                SnackbarManager.show(
                                        Snackbar.with(getApplicationContext())
                                                .text("Invalid Student Number"), Login.this);
                            } else {
                                SnackbarManager.show(
                                        Snackbar.with(getApplicationContext())
                                                .text("Student Number does not exist."), Login.this);
                            }

                            return true;
                        default:
                            break;
                    }
                }
                return false;
            }
        });

//        username.setText(R.string.clarky);
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
        if (activeNetwork != null && activeNetwork.isConnected()) {
            return true;
        }
        return false;
    }


    public void login(View v) {

        if (hasInternetConnection()) {

            CheckEditTextIsEmptyOrNot();

            if (CheckEditText) {

                UserLoginFunction(usernameHolder);

            } else if (usernameHolder.length() != 9) {

                SnackbarManager.show(
                        Snackbar.with(getApplicationContext())
                                .text("Invalid Student Number"), Login.this);
            } else {

                SnackbarManager.show(
                        Snackbar.with(getApplicationContext())
                                .text("Student Number does not exist."), Login.this);
            }
        } else {
            SnackbarManager.show(
                    Snackbar.with(getApplicationContext())
                            .text("No Internet Connection"), Login.this);
        }
    }


    public void CheckEditTextIsEmptyOrNot() {

        usernameHolder = username.getText().toString();

        if (TextUtils.isEmpty(usernameHolder)) {
            CheckEditText = false;
        } else if (usernameHolder.length() != 9) {
            CheckEditText = false;
        } else {
            CheckEditText = true;
        }
    }

    public void UserLoginFunction(final String username) {

        class UserLoginClass extends AsyncTask<String, Void, String> {

            @Override
            protected void onPreExecute() {
                super.onPreExecute();

                progressDialog = new ProgressDialog(Login.this, R.style.MyTheme);
                progressDialog.setCancelable(false);
                progressDialog.setProgressStyle(android.R.style.Widget_ProgressBar_Small);
                progressDialog.show();

            }

            @Override
            protected void onPostExecute(String httpResponseMsg) {

                super.onPostExecute(httpResponseMsg);

                progressDialog.dismiss();


                if (httpResponseMsg.equalsIgnoreCase("Data Matched")) {

                    Bundle b = new Bundle();

                    b.putString("num", usernameHolder);

                    Intent intent = new Intent(Login.this, Screen_Home.class);
                    intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                    intent.putExtra("EXIT", true);
                    intent.putExtras(b);

                    startActivity(intent);
                    overridePendingTransition(R.anim.push_left_in, R.anim.push_left_out);


                } else {
                    SnackbarManager.show(
                            Snackbar.with(getApplicationContext())
                                    .text(httpResponseMsg), Login.this);
                }

            }

            @Override
            protected String doInBackground(String... params) {


                hashMap.put("studnum", params[0]);

                finalResult = httpParse.postRequest(hashMap, HttpURL);

                return finalResult;

            }

        }

        UserLoginClass userLoginClass = new UserLoginClass();

        userLoginClass.execute(username);
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
    }
}
