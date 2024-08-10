package com.FEUTech.sterben.iqueue;

import retrofit.Callback;
import retrofit.client.Response;
import retrofit.http.Field;
import retrofit.http.FormUrlEncoded;
import retrofit.http.POST;

public interface Queue_Accounting_Send_Data {
    @FormUrlEncoded
    @POST("/steb/addToAccounting.php")
    public void insertUserAccounting(

            @Field("id") String id,
            @Field("fireID") String fireID,
            @Field("datacode") String datacode,
            @Field("top") String top,
            @Field("course") String course,
            @Field("firstname") String firstname,
            @Field("middlename") String middlename,
            @Field("lastname") String lastname,
            @Field("current_schoolyear") String current_schoolyear,
            @Field("current_term") String current_term,
            @Field("contactnum") String contactnum,
            Callback<Response> callback);
}
