package com.FEUTech.sterben.iqueue;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RadioGroup;

import com.nispok.snackbar.Snackbar;
import com.nispok.snackbar.SnackbarManager;

public class Queue_Cashier1_add extends AppCompatActivity {

    private RadioGroup radioGroup;
    private EditText others;
    private Button addAnother;
    private String transaction;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_queue__cashier1_add);
//        getWindow().setFlags(WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS, WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS);
        radioGroup = (RadioGroup) findViewById(R.id.radioGroup11);
        others = (EditText) findViewById(R.id.add_another_to_cashier);
        addAnother = (Button) findViewById(R.id.concat);

        radioListener();
        add();

    }

    private void add() {
        addAnother.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int hep = 0;
                switch (radioGroup.getCheckedRadioButtonId()) {
                    case -1:
                        SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"),
                                Queue_Cashier1_add.this);
                        break;
                    case R.id.rb1:
                        transaction = "Down Payment";
                        hep = hep + 1;
                        break;
                    case R.id.rb2:
                        transaction = "Balance";
                        hep = hep + 1;
                        break;
                    case R.id.rb3:
                        if (others.getText().toString().isEmpty()) {
                            SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"),
                                    Queue_Cashier1_add.this);
                        } else {
                            transaction = others.getText().toString();
                            hep = hep + 1;
                        }
                        break;
                    default:
                        SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"),
                                Queue_Cashier1_add.this);
                        break;
                }
                if (hep == 1) {
                    Intent passData = new Intent(Queue_Cashier1_add.this, Queue_Cashier1.class);
                    Bundle bundle = new Bundle();
                    bundle.putString("anotherTransaction", transaction);
                    passData.putExtras(bundle);
                    startActivity(passData);
                    finish();
                } else {
                    SnackbarManager.show(Snackbar.with(getApplicationContext()).text("Please fill all fields"),
                            Queue_Cashier1_add.this);
                }
            }
        });
    }

    private void radioListener() {
        radioGroup.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup radioGroup, int i) {
                int id = radioGroup.getCheckedRadioButtonId();
                switch (id) {
                    case R.id.rb1:
                        others.setVisibility(View.GONE);
                        break;
                    case R.id.rb2:
                        others.setVisibility(View.GONE);
                        break;
                    case R.id.rb3:
                        others.setVisibility(View.VISIBLE);
                        break;
                    default:
                        others.setVisibility(View.GONE);
                        break;
                }
            }
        });
    }

    @Override
    public void onBackPressed() {
        finish();
    }
}
