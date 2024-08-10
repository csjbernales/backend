package com.FEUTech.sterben.iqueue;

import retrofit.Callback;
import retrofit.client.Response;
import retrofit.http.Field;
import retrofit.http.FormUrlEncoded;
import retrofit.http.POST;

public interface Queue_Cashier_Send_Data {
    @FormUrlEncoded
    @POST("/steb/addToCashier.php")
    public void insertUser(

            @Field("id") String id,
            @Field("contactnum") String contactnum,
            @Field("datacode") String datacode,
            @Field("fireID") String fireID,
            @Field("term") String term,
            @Field("top") String top,
            @Field("year") String year,
            @Field("course") String course,
            @Field("firstname") String firstname,
            @Field("middlename") String middlename,
            @Field("lastname") String lastname,
            @Field("type") String type,
            @Field("branch") String branch,
            @Field("checknum") String checknum,
            @Field("totalamount") String totalamount,
            Callback<Response> callback);
}
