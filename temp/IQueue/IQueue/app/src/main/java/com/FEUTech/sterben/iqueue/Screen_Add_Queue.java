package com.FEUTech.sterben.iqueue;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.BottomNavigationView;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.MenuItem;
import android.view.View;
import android.widget.Adapter;

import co.ceryle.fitgridview.FitGridView;
import me.drakeet.materialdialog.MaterialDialog;

public class Screen_Add_Queue extends AppCompatActivity {

    MaterialDialog mMaterialDialog;
    Context context = this;
    private FitGridView gridView;
    private BottomNavigationView.OnNavigationItemSelectedListener mOnNavigationItemSelectedListener
            = new BottomNavigationView.OnNavigationItemSelectedListener() {

        @Override
        public boolean onNavigationItemSelected(@NonNull MenuItem item) {
            switch (item.getItemId()) {
                case R.id.home:
                    Intent in = getIntent();
                    Bundle b = in.getExtras();
                    String s = b.getString("num");
                    Bundle bundle = new Bundle();
                    bundle.putString("num", s);
                    Intent intent = new Intent(Screen_Add_Queue.this, Screen_Home.class);
                    intent.putExtras(bundle);
                    startActivity(intent);
                    overridePendingTransition(0, 0);
                    return true;
                case R.id.addqueue:

                    return true;
                case R.id.myqueue:
                    Intent in2 = getIntent();
                    Bundle b2 = in2.getExtras();
                    String s2 = b2.getString("num");
                    Bundle bundle2 = new Bundle();
                    bundle2.putString("num", s2);
                    Intent intent2 = new Intent(Screen_Add_Queue.this, Screen_My_Queue.class);
                    intent2.putExtras(bundle2);
                    startActivity(intent2);
                    overridePendingTransition(0, 0);
                    return true;
            }
            return false;
        }
    };

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.screen_add_queue);

        BottomNavigationView navigation = (BottomNavigationView) findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);
        navigation.setSelectedItemId(R.id.addqueue);

        gridView = (FitGridView) findViewById(R.id.gridView);
        gridView.setFitGridAdapter(new com.FEUTech.sterben.iqueue.Adapter(this));
        changeSize(2, 2);

        Intent in2 = getIntent();
        Bundle b2 = in2.getExtras();
        String s2 = b2.getString("num");
        Bundle bundle2 = new Bundle();
        bundle2.putString("num", s2);
        Intent intent2 = new Intent(Screen_Add_Queue.this, Adapter.class);
        intent2.putExtras(bundle2);
    }

    private void showAlert() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setNegativeButton(android.R.string.no, new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
            }
        });

        gridView.setFitGridAdapter(new com.FEUTech.sterben.iqueue.Adapter(this));
        builder.setView(gridView);
        builder.show();

    }

    public void onClick(View v) {
        showAlert();
    }

    private void changeSize(int r, int c) {
        gridView.setNumRows(r);
        gridView.setNumColumns(c);
        gridView.update();
    }

    @Override
    public void onBackPressed() {
        mMaterialDialog = new MaterialDialog(this)
                .setTitle("iQueue")
                .setMessage("Quit application?")
                .setPositiveButton("Exit", new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        Intent intent = new Intent(Screen_Add_Queue.this, Login.class);
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

}
