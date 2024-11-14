package com.maxholmes.androidapp.screen.detail.court

import com.maxholmes.androidapp.utils.base.BasePresenter

class CourtDetailContract {
    interface View {

        fun onError(exception: Exception?)
    }

    interface Presenter : BasePresenter<View> {
    }
}