//
//  SignupViewController.swift
//  Hangout
//
//  Created by Vahid Mazdeh on 11/7/14.
//  Copyright (c) 2014 Peyman Mortazavi. All rights reserved.
//

import Foundation
import UIKit


class SignupViewController: UIViewController {
    
    @IBOutlet weak var firstField: UITextField!
    @IBOutlet weak var lastField: UITextField!
    @IBOutlet weak var emailField: UITextField!
    @IBOutlet weak var passwordField: UITextField!
    @IBOutlet weak var phoneField: UITextField!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
    @IBAction func AttemptToSignUp(sender: UIButton) {
        let url = NSURL(string: HangoutClient.ApiUrl + "accounts/")!
        var request: NSMutableURLRequest = NSMutableURLRequest(URL: url)
        request.HTTPMethod = "POST"
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        let signupDictionary = ["firstname": firstField.text!, "lastname": lastField.text!, "phonenumber":phoneField.text, "email":emailField.text, "password":passwordField.text]
        let data = NSJSONSerialization.dataWithJSONObject(signupDictionary, options: NSJSONWritingOptions(0), error: nil)
        request.HTTPBody = data
        var response: AutoreleasingUnsafeMutablePointer <NSURLResponse?> = nil
        var error: AutoreleasingUnsafeMutablePointer<NSError?> = nil
        
        var dataVal: NSData? = NSURLConnection.sendSynchronousRequest(request, returningResponse: response, error:error)
        
        if let data = dataVal{
            let json = NSJSONSerialization.JSONObjectWithData(data, options: NSJSONReadingOptions.MutableContainers, error: nil) as NSDictionary
            println(json)
            let loginIdOpt = json["id"] as NSString?
            if let id = loginIdOpt {
                //We have an id!
                self.performSegueWithIdentifier("SignupToFriends", sender: self)
                /*
                let urlLogin = NSURL(string: HangoutClient.ApiUrl + "accounts/authenticate")!
                var requestLogin: NSMutableURLRequest = NSMutableURLRequest(URL: urlLogin)
                requestLogin.HTTPMethod = "POST"
                requestLogin.addValue("application/json", forHTTPHeaderField: "Content-Type")
                let loginDictionary = ["email": emailField.text!, "password": passwordField.text!]
                let dataLogin = NSJSONSerialization.dataWithJSONObject(loginDictionary, options: NSJSONWritingOptions(0), error: nil)
                requestLogin.HTTPBody = dataLogin
                var responseLogin: AutoreleasingUnsafeMutablePointer <NSURLResponse?> = nil
                var errorLogin: AutoreleasingUnsafeMutablePointer<NSError?> = nil
                
                var dataValLogin: NSData? = NSURLConnection.sendSynchronousRequest(request, returningResponse: response, error:errorLogin)
                
                if let dataLogin = dataValLogin{
                    let jsonLogin = NSJSONSerialization.JSONObjectWithData(dataLogin, options: NSJSONReadingOptions.MutableContainers, error: nil) as NSDictionary
                    println(jsonLogin)
                    let firstNameOpt = jsonLogin["firstName"] as NSString?
                    if let firstName = firstNameOpt{
                        //We have a name!
                        self.performSegueWithIdentifier("SignupToFriends", sender: self)
                        println("Performed segue with identifier signup to friends")
                    }else{
                        //We have an error!
                    }
                    //Either use UIAlertView to show error, or call self.presentViewController(the other view controller)
                }else{
                    //Use UIAlertView to show error
                }*/
                
            } else {
                //We have an error!
            }
            //Either use UIAlertView to show error, or call self.presentViewController(the other view controller)
        }else{
            //Use UIAlertView to show error
        }
        
        // Gather all text fields (first, last, email, password, phone number)
        // Check that phone number is only numerical characters
        // Check that all fields are non-empty
        // Format into JSON
        // Call CreateUser(CreateUserModel model)
        // If successful
            // Go to FriendsViewController
        // Otherwise, print a message saying this should never happen
    }
    
    @IBAction func dismiss(sender: UIButton) {
        self.dismissViewControllerAnimated(true, completion: nil)
    }
    
    
}
