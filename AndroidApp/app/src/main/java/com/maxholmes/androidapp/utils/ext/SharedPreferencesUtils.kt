package com.maxholmes.androidapp.utils.ext

import android.content.Context
import android.content.SharedPreferences

object SharedPreferencesUtils {

    private const val PREFS_NAME = "app_prefs"
    private const val TOKEN_KEY = "TOKEN"

    fun saveToken(context: Context, token: String) {
        val sharedPreferences: SharedPreferences = context.getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE)
        sharedPreferences.edit().putString(TOKEN_KEY, token).apply()
    }

    fun getToken(context: Context): String? {
        val sharedPreferences: SharedPreferences = context.getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE)
        return sharedPreferences.getString(TOKEN_KEY, null)
    }

    fun clearToken(context: Context) {
        val sharedPreferences: SharedPreferences = context.getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE)
        sharedPreferences.edit().remove(TOKEN_KEY).apply()
    }
}