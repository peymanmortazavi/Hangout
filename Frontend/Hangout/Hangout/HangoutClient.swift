//
//  HangoutClient.swift
//  Hangout
//
//  Created by Vahid Mazdeh on 11/7/14.
//  Copyright (c) 2014 Peyman Mortazavi. All rights reserved.
//

import Foundation

public class HangoutClient {
    
    public var ApiUrl: String = ""
    
    init(apiUrl: String) {
        ApiUrl = apiUrl
    }
    
    func testGet () -> String {
        let urlPath: String = ApiUrl + "accounts/test"
        var url: NSURL = NSURL(string: urlPath)!
        var request1: NSURLRequest = NSURLRequest(URL: url)
        var response: AutoreleasingUnsafeMutablePointer <NSURLResponse?
        >=nil
        
        var error: AutoreleasingUnsafeMutablePointer <NSErrorPointer?>=nil
        var dataVal: NSData =  NSURLConnection.sendSynchronousRequest(request1, returningResponse: response, error:nil)!
        var err: NSError
        var jsonResult: NSDictionary = NSJSONSerialization.JSONObjectWithData(dataVal, options: NSJSONReadingOptions.MutableContainers, error: nil) as NSDictionary
        println("Synchronous\(jsonResult)")
        
        return jsonResult["firstName"] as NSString;
    }
    
}