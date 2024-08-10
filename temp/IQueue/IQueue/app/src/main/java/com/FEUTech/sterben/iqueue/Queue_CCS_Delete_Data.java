package com.FEUTech.sterben.iqueue;

import retrofit.Callback;
import retrofit.client.Response;
import retrofit.http.Field;
import retrofit.http.FormUrlEncoded;
import retrofit.http.POST;

public interface Queue_CCS_Delete_Data {
    @FormUrlEncoded
    @POST("/steb/deletecurrentqueue_ccs.php")
    public void deleteUserfromCCS(

            @Field("number") String number,
            Callback<Response> callback);
}
