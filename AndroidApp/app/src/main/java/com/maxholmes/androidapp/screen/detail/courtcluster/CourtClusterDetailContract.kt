package com.maxholmes.androidapp.screen.detail.courtcluster

import com.maxholmes.androidapp.utils.base.BasePresenter

class CourtClusterDetailContract {
    interface View {

        fun onError(exception: Exception?)
    }

    interface Presenter : BasePresenter<View> {
    }
}