package com.maxholmes.androidapp.screen.home


import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.utils.base.BasePresenter

class HomeContract {
    interface View {
//        fun onGetCourtSuccess(courts: MutableList<Court>)

        fun onError(exception: Exception?)
    }

    interface Presenter : BasePresenter<View> {
    }
}
