#Clone the public repo in your current repo
GIT=`which git`
PUBLIC_REPO_NAME="unity-openwrap-sdk-samples"
    
$GIT clone git@github.com:vishalpatil0/$PUBLIC_REPO_NAME.git
echo "cloned......."
#Copy sample app from internal repo to public repo
INTERNAL_REPO_SAMPLE_APP="SampleApps/"
PUBLIC_REPO_DEST="$PUBLIC_REPO_NAME/"
    
cp -R $INTERNAL_REPO_SAMPLE_APP $PUBLIC_REPO_DEST
echo "copied......"
#Push the changes to the public repo
cd $PUBLIC_REPO_NAME
$GIT checkout -b mainTest
$GIT add .
$GIT commit -m "Release $SDK_UNITY_PACKAGE_VERSION"
$GIT push -u origin mainTest
echo "pushedd......"
#Delete the public repo from the internal repo
cd ..
rm -rf $PUBLIC_REPO_NAME