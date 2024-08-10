package com.FEUTech.sterben.iqueue;

import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.BottomNavigationView;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.text.Html;
import android.text.method.LinkMovementMethod;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.nispok.snackbar.Snackbar;
import com.nispok.snackbar.SnackbarManager;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.InputStream;
import java.util.HashMap;

import me.drakeet.materialdialog.MaterialDialog;

public class Screen_Home extends AppCompatActivity {

    static TextView VFname, Vcourseyear, Vusername, Canvas;
    static String studentID, finalResult;
    private static Context context;
    HttpParse httpParse = new HttpParse();
    String HttpURL = "http://iqueuesystem.com/steb/deleteprofilepicture.php";
    HashMap<String, String> hashMap = new HashMap<>();
    MaterialDialog mMaterialDialog;
    ProgressDialog progressDialog;/*
    Runnable runnable;
    Handler handler = new Handler();*/
    private MaterialDialog mMaterialDialog2;
    private ImageView mImage;
    private Uri mImageUri;
    private ViewPager mViewPager;
    private Screen_Home.SectionsPagerAdapter mSectionsPagerAdapter;
    private BottomNavigationView.OnNavigationItemSelectedListener mOnNavigationItemSelectedListener
            = new BottomNavigationView.OnNavigationItemSelectedListener() {

        @Override
        public boolean onNavigationItemSelected(@NonNull MenuItem item) {
            switch (item.getItemId()) {
                case R.id.home:
                    return true;
                case R.id.addqueue:
                    Intent in = getIntent();
                    Bundle b = in.getExtras();
                    String s = b.getString("num");
                    Bundle bundle = new Bundle();
                    bundle.putString("num", s);
                    Intent intent = new Intent(Screen_Home.this, Screen_Add_Queue.class);
                    intent.putExtras(bundle);
                    startActivity(intent);
                    overridePendingTransition(0, 0);
                    return true;
                case R.id.myqueue:
                    Intent in2 = getIntent();
                    Bundle b2 = in2.getExtras();
                    String s2 = b2.getString("num");
                    Bundle bundle2 = new Bundle();
                    bundle2.putString("num", s2);
                    Intent intent2 = new Intent(Screen_Home.this, Screen_My_Queue.class);
                    intent2.putExtras(bundle2);
                    startActivity(intent2);
                    overridePendingTransition(0, 0);
                    return true;
            }
            return false;
        }
    };

    public static Context getAppContext() {
        return Screen_Home.context;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.screen_home);
        /*
        progressDialog = new ProgressDialog(Screen_Home.this,R.style.MyTheme);
        progressDialog.setCancelable(false);
        progressDialog.setProgressStyle(android.R.style.Widget_ProgressBar_Small);
        progressDialog.show();*/
        BottomNavigationView navigation = (BottomNavigationView) findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);
        navigation.setSelectedItemId(R.id.home);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        mSectionsPagerAdapter = new Screen_Home.SectionsPagerAdapter(getSupportFragmentManager());
        mViewPager = (ViewPager) findViewById(R.id.container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        TabLayout tabLayout = (TabLayout) findViewById(R.id.tabs);
        mViewPager.addOnPageChangeListener(new TabLayout.TabLayoutOnPageChangeListener(tabLayout));
        tabLayout.addOnTabSelectedListener(new TabLayout.ViewPagerOnTabSelectedListener(mViewPager));

        Intent in2 = getIntent();
        Bundle b2 = in2.getExtras();
        studentID = b2.getString("num");

        Screen_Home.context = getApplicationContext();

/*
        runnable = new Runnable() {
            @Override
            public void run() {
                //function
                progressDialog.dismiss();
            }
        };
        handler.postDelayed(runnable, 3000);*/
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @Override
    public void onBackPressed() {
        mMaterialDialog = new MaterialDialog(this)
                .setTitle("iQueue")
                .setMessage("Quit application?")
                .setPositiveButton("Exit", new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        Intent intent = new Intent(Screen_Home.this, Login.class);
                        startActivity(intent);
                        overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
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

    public void UserLoginFunction(final String email) {

        class UserLoginClass2 extends AsyncTask<String, Void, String> {

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
//
                progressDialog = new ProgressDialog(Screen_Home.this, R.style.MyTheme);
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

                    mMaterialDialog2 = new MaterialDialog(Screen_Home.this)
                            .setTitle("Change profile picture")
                            .setPositiveButton("OK", new View.OnClickListener() {
                                @Override
                                public void onClick(View v) {
//                                    Bundle bi = new Bundle();
//                                    bi.putString("num", studentID);
//                                    Intent i = new Intent(Screen_Home.this, Screen_Add_Queue.class);
//                                    i.putExtras(bi);
//                                    startActivity(i);
//                                    overridePendingTransition(R.anim.push_right_in, R.anim.push_right_out);
//                                    mMaterialDialog2.dismiss();
                                    mMaterialDialog2.dismiss();
//                                    finish();
                                }
                            }).setNegativeButton("Cancel", new View.OnClickListener() {
                                @Override
                                public void onClick(View v) {
                                    mMaterialDialog2.dismiss();
                                }
                            });
                    mMaterialDialog2.show();

                } else {

                    UserLoginFunction(studentID);

                }
            }

            @Override
            protected String doInBackground(String... params) {


                hashMap.put("studnum", params[0]);

                finalResult = httpParse.postRequest(hashMap, HttpURL);

                return finalResult;

            }

        }

        UserLoginClass2 userLoginClass2 = new UserLoginClass2();
        userLoginClass2.execute(email);
    }

    /**
     * A placeholder fragment containing a simple view.
     */
    public static class PlaceholderFragment extends Fragment {
        /**
         * The fragment argument representing the section number for this
         * fragment.
         */
        private static final String ARG_SECTION_NUMBER = "section_number";
        String imageLocationHolder;
        String imageURL = "http://iqueuesystem.com/uploads/";

        public PlaceholderFragment() {
        }

        /**
         * Returns a new instance of this fragment for the given section
         * number.
         */
        public static Screen_Home.PlaceholderFragment newInstance(int sectionNumber) {
            Screen_Home.PlaceholderFragment fragment = new Screen_Home.PlaceholderFragment();
            Bundle args = new Bundle();
            args.putInt(ARG_SECTION_NUMBER, sectionNumber);
            fragment.setArguments(args);
            return fragment;
        }

        @Override
        public View onCreateView(LayoutInflater inflater, ViewGroup container,
                                 Bundle savedInstanceState) {
            //TextView textView = rootView.findViewById(R.id.section_label);
            //textView.setText(getString(R.string.section_format, getArguments().getInt(ARG_SECTION_NUMBER)));
            switch (getArguments().getInt(ARG_SECTION_NUMBER)) {
                case 1:
                    View contact = inflater.inflate(R.layout.fragment_contact, container, false);
                    Canvas = (TextView) contact.findViewById(R.id.canvas_link);

                    String CanvasLink = "<a href='http://iqueuesystem.com'>Open in web browser? Click here</a>";

                    Canvas.setText(Html.fromHtml(CanvasLink));
                    Canvas.setMovementMethod(LinkMovementMethod.getInstance());
                    return contact;
                case 2:
                    final View profile = inflater.inflate(R.layout.fragment_profile, container, false);
                    VFname = (TextView) profile.findViewById(R.id.user_Fullname);
                    Vusername = (TextView) profile.findViewById(R.id.user_name);
                    Vcourseyear = (TextView) profile.findViewById(R.id.user_courseyear);
                    String number = studentID;
                    String url = "http://iqueuesystem.com/steb/getStudentdata.php?studentID=" + number;
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

                                            String imageLocationHolderplaceholder = jsonobject.getString(("user_img"));
                                            imageLocationHolder = imageLocationHolderplaceholder;
                                            String fname = jsonobject.getString("firstname");
                                            String lname = jsonobject.getString("lastname");
                                            String username = jsonobject.getString("stud_num").trim();

                                            String course = jsonobject.getString("program").trim();
                                            String year = jsonobject.getString("year_level").trim();

                                            VFname.setText(fname + " " + lname);
                                            Vusername.setText(username);

                                            Vcourseyear.setText(course + "\n" + year);


                                            new DownloadImageFromInternet((ImageView) profile.findViewById(R.id.profilePicture))
                                                    .execute(imageURL + imageLocationHolder);
                                        }
                                    } catch (JSONException e) {
                                        e.printStackTrace();
                                        SnackbarManager.show(
                                                Snackbar.with(getAppContext())
                                                        .text("Something went wrong on fetching data"));

                                    }
                                }
                            },
                            new Response.ErrorListener() {
                                @Override
                                public void onErrorResponse(VolleyError error) {
                                    if (error != null) {

                                        SnackbarManager.show(
                                                Snackbar.with(getAppContext())
                                                        .text("Network Problem"));
                                    }
                                }
                            }

                    );

                    MySingleton.getInstance(getAppContext()).addToRequestQueue(stringRequest);


                    return profile;
                case 3:
                    View about = inflater.inflate(R.layout.fragment_about, container, false);
                    return about;
                default:
                    View default_fragment = inflater.inflate(R.layout.fragment_profile, container, false);
                    return default_fragment;
            }
        }
    }

    private static class DownloadImageFromInternet extends AsyncTask<String, Void, Bitmap> {
        @SuppressLint("StaticFieldLeak")
        ImageView imageView;

        private DownloadImageFromInternet(ImageView imageView) {
            this.imageView = imageView;
            //Toast.makeText(getAppContext(), "Image loading...", Toast.LENGTH_SHORT).show();
        }

        protected Bitmap doInBackground(String... urls) {
            String imageURL = urls[0];
            Bitmap bimage = null;
            try {
                InputStream in = new java.net.URL(imageURL).openStream();
                bimage = BitmapFactory.decodeStream(in);

            } catch (Exception e) {
//                Toast.makeText(getAppContext(), "You do not have profile picture yet", Toast.LENGTH_SHORT).show();
                //Log.e("Error Message", e.getMessage());
                e.printStackTrace();
            }
            return bimage;
        }

        protected void onPostExecute(Bitmap result) {

            imageView.setImageBitmap(result);

        }
    }

    /**
     * A {@link FragmentPagerAdapter} that returns a fragment corresponding to
     * one of the sections/tabs/pages.
     */
    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        private SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            // getItem is called to instantiate the fragment for the given page.
            // Return a PlaceholderFragment (defined as a static inner class below).
            return Screen_Home.PlaceholderFragment.newInstance(position + 1);
        }

        @Override
        public int getCount() {
            // Show 3 total pages.
            return 3;
        }
    }

}
