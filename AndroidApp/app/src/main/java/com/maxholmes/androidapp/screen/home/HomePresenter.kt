package com.maxholmes.androidapp.screen.home

import com.maxholmes.androidapp.data.repository.CourtRepository

class HomePresenter internal constructor(private val mCourtRepository: CourtRepository) :
    HomeContract.Presenter {
    private var mView: HomeContract.View? = null

    override fun onStart() {
        TODO("Not yet implemented")
    }

    override fun onStop() {
        TODO("Not yet implemented")
    }

    override fun setView(view: HomeContract.View?) {
        this.mView = view
    }

}
