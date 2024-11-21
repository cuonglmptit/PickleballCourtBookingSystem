package com.maxholmes.androidapp.screen.menu

import com.maxholmes.androidapp.utils.base.BasePresenter

class MenuContract {
    interface View {

        fun onError(exception: Exception?)
    }

    interface Presenter : BasePresenter<View> {
    }
}